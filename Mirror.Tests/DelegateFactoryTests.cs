using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Mirror.Tests
{
	public class DelegateFactoryTests
	{

		public static IEnumerable<object[]> TestCases
		{
			get
			{
				var reflectionMethods = from m in typeof(DelegateFactory).GetRuntimeMethods()
										where m.IsPublic
										where m.Name == nameof(DelegateFactory.Create)
										select m;

				var methodDescriptors = new List<MethodDescriptor>();
				foreach (var methodInfo in reflectionMethods)
				{
					var methodName = $"{methodInfo.Name}With{methodInfo.GetParameters().Length - 3}ParameterTypes";
					var parameters = new Type[methodInfo.GetParameters().Length - 3];
					for (var i = 0; i < parameters.Length; i++)
					{
						parameters[i] = typeof(int);
					}
					methodDescriptors.Add(new MethodDescriptor(methodName, parameters));
					methodDescriptors.Add(new MethodDescriptor(methodName, parameters, new[] { typeof(int) }));
				}

				var typeBuilder = new TestTypeBuilder();
				var type = typeBuilder.CreateClass("GetDelegateDynamicTestClass", methodDescriptors);
				var instance = Activator.CreateInstance(type);

				var methodsToTest = from m in type.GetRuntimeMethods()
									where m.IsPublic
									where m.ReturnType == typeof(MethodCallInfo)
									select m;

				foreach (var methodInfo in methodsToTest)
				{

					var parameterValues = new object[methodInfo.GetParameters().Length];
					for (var i = 0; i < parameterValues.Length; i++)
					{
						parameterValues[i] = i;
					}

					

					if (methodInfo.IsGenericMethod)
					{
						var genericMethod = methodInfo.MakeGenericMethod(typeof(int));
						var expectedReturn = (MethodCallInfo)genericMethod.Invoke(instance, parameterValues);
						yield return new object[] { new GetDelegateTestCase(instance, parameterValues, genericMethod.Name, expectedReturn, genericMethod.Name + "Generic", new[] { typeof(int) }) };
					}
					else
					{
						var expectedReturn = (MethodCallInfo)methodInfo.Invoke(instance, parameterValues);
						yield return new object[] { new GetDelegateTestCase(instance, parameterValues, methodInfo.Name, expectedReturn, methodInfo.Name) };
					}
				}
			}
		}

		public class GetDelegateTestCase
		{
			public GetDelegateTestCase(object objInstance, object[] methodParameters, string methodName, MethodCallInfo expectedReturn, string name, Type[] genericMethodParameters = null)
			{
				ObjInstance = objInstance;
				MethodParameters = methodParameters;
				MethodName = methodName;
				ExpectedReturn = expectedReturn;
				Name = name;
				GenericMethodParameters = genericMethodParameters ?? new Type[0];
			}

			public string MethodName { get; }
			public object[] MethodParameters { get; }
			public object ObjInstance { get; }
			public Type[] GenericMethodParameters { get; }
			public MethodCallInfo ExpectedReturn { get; }
			public string Name { get; }
			public override string ToString() => Name;
		}

		[Theory]
		[MemberData(nameof(TestCases))]
		public void GetDelegate(GetDelegateTestCase delegateTestCase)
		{
			var parameterTypes = new Type[delegateTestCase.MethodParameters.Length + 3];
			parameterTypes[0] = typeof(Type);
			parameterTypes[1] = typeof(string);
			for (var i = 0; i < delegateTestCase.MethodParameters.Length; i++)
			{
				parameterTypes[i + 2] = typeof(Type);
			}
			parameterTypes[parameterTypes.Length - 1] = typeof(Type[]);


			var parametersValues = new object[delegateTestCase.MethodParameters.Length + 3];
			parametersValues[0] = delegateTestCase.ObjInstance.GetType();
			parametersValues[1] = delegateTestCase.MethodName;
			for (var i = 0; i < delegateTestCase.MethodParameters.Length; i++)
			{
				parametersValues[i + 2] = delegateTestCase.MethodParameters[i].GetType();
			}
			parametersValues[parametersValues.Length - 1] = delegateTestCase.GenericMethodParameters;

			var delegateFactoryMethod = typeof(DelegateFactory).GetRuntimeMethod(nameof(DelegateFactory.Create), parameterTypes);
			var methodDelegate = delegateFactoryMethod.Invoke(delegateTestCase.ObjInstance, parametersValues);
			var funcInvokeMethod = methodDelegate.GetType().GetRuntimeMethods().First(x => x.Name == nameof(Func<int>.Invoke));
			var returnValue = (MethodCallInfo)funcInvokeMethod.Invoke(methodDelegate, new[] { delegateTestCase.ObjInstance }.Concat(delegateTestCase.MethodParameters).ToArray());

			Assert.Equal(delegateTestCase.ExpectedReturn.MethodName, returnValue.MethodName);
			CheckParameterValues(delegateTestCase.ExpectedReturn, returnValue);
			CheckGenericTypeParameters(delegateTestCase.ExpectedReturn, returnValue);
		}

		private void CheckParameterValues(MethodCallInfo expected, MethodCallInfo actual)
		{
			Assert.Equal(expected.ParameterValues.Length, actual.ParameterValues.Length);
			for (int i = 0; i < expected.ParameterValues.Length; i++)
			{
				Assert.Equal(expected.ParameterValues[i], actual.ParameterValues[i]);
			}
		}

		private void CheckGenericTypeParameters(MethodCallInfo expected, MethodCallInfo actual)
		{
			Assert.Equal(expected.GenericMethodParameters.Length, actual.GenericMethodParameters.Length);

			for (int i = 0; i < expected.GenericMethodParameters.Length; i++)
			{
				Assert.Equal(expected.GenericMethodParameters[i].FullName, actual.GenericMethodParameters[i].FullName);
			}
		}
	}
}
