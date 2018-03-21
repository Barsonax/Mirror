using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;

namespace Mirror.Tests
{
	public class DelegateFactoryTests
	{
		public static IEnumerable<object[]> TestCases
		{
			get
			{
				var cachedReflectionMethods = from m in typeof(DelegateFactory).GetRuntimeMethods()
											  where m.IsPublic
											  where m.Name == nameof(DelegateFactory.Create)
											  select m;

				foreach (var methodInfo in cachedReflectionMethods)
				{
					var methodDescriptors = new List<MethodDescriptor>();
					var methodName = $"{methodInfo.Name}With{methodInfo.GetParameters().Length - 3}ParameterTypes";
					var parameters = new Type[methodInfo.GetParameters().Length - 3];
					for (var i = 0; i < parameters.Length; i++)
					{
						parameters[i] = typeof(int);
					}

					var parameterValues = new object[parameters.Length];
					for (var i = 0; i < parameterValues.Length; i++)
					{
						parameterValues[i] = 0;
					}
					methodDescriptors.Add(new MethodDescriptor(methodName, typeof(int), parameters));
					var obj = MyTypeBuilder.CreateNewObject(methodDescriptors);
					var value = obj.GetType().GetRuntimeMethod(methodName, parameters).Invoke(obj, parameterValues);

					yield return new object[] { new GetDelegateTestCase(obj, parameterValues, methodName, value) };
				}
			}
		}

		public class GetDelegateTestCase
		{
			public GetDelegateTestCase(object objInstance, object[] methodParameters, string methodName, object expectedReturn)
			{
				ObjInstance = objInstance;
				MethodParameters = methodParameters;
				MethodName = methodName;
				ExpectedReturn = expectedReturn;
			}

			public string MethodName { get; }
			public object[] MethodParameters { get; }
			public object ObjInstance { get; }
			public object ExpectedReturn { get; }

			public override string ToString()
			{
				return MethodName;
			}
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

			var delegateFactoryMethod = typeof(DelegateFactory).GetRuntimeMethod(nameof(DelegateFactory.Create), parameterTypes);
			var methodDelegate = delegateFactoryMethod.Invoke(delegateTestCase.ObjInstance, parametersValues);
			var funcInvokeMethod = methodDelegate.GetType().GetRuntimeMethods().First(x => x.Name == nameof(Func<int>.Invoke));
			var returnValue = funcInvokeMethod.Invoke(methodDelegate, new[] { delegateTestCase.ObjInstance }.Concat(delegateTestCase.MethodParameters).ToArray());

			Assert.Equal(delegateTestCase.ExpectedReturn, returnValue);
		}
	}

	public class FieldDescriptor
	{
		public FieldDescriptor(string fieldName, Type fieldType)
		{
			FieldName = fieldName;
			FieldType = fieldType;
		}
		public string FieldName { get; }
		public Type FieldType { get; }
	}

	public class MethodDescriptor
	{
		public string MethodName { get; }
		public Type ReturnType { get; }
		public Type[] Parameters { get; }

		public MethodDescriptor(string methodName, Type returnType, Type[] parameters)
		{
			MethodName = methodName;
			ReturnType = returnType;
			Parameters = parameters;
		}
	}

	public static class MyTypeBuilder
	{
		public static object CreateNewObject(List<MethodDescriptor> methods)
		{
			var myTypeInfo = CompileResultTypeInfo(methods);
			var myType = myTypeInfo.AsType();
			var myObject = Activator.CreateInstance(myType);

			return myObject;
		}

		public static TypeInfo CompileResultTypeInfo(List<MethodDescriptor> methods)
		{
			var tb = GetTypeBuilder();
			var constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			foreach (var methodDescriptor in methods)
			{
				CreateMethod(tb, methodDescriptor.MethodName, methodDescriptor.ReturnType, methodDescriptor.Parameters);
			}

			var objectTypeInfo = tb.CreateTypeInfo();
			return objectTypeInfo;
		}

		private static TypeBuilder GetTypeBuilder()
		{
			var typeSignature = "MyDynamicType";
			var an = new AssemblyName(typeSignature);
			var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(Guid.NewGuid().ToString()), AssemblyBuilderAccess.Run);
			var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
			var tb = moduleBuilder.DefineType(typeSignature,
					TypeAttributes.Public |
					TypeAttributes.Class |
					TypeAttributes.AutoClass |
					TypeAttributes.AnsiClass |
					TypeAttributes.BeforeFieldInit |
					TypeAttributes.AutoLayout,
					null);
			return tb;
		}

		private static void CreateMethod(TypeBuilder tb, string methodName, Type returnType, Type[] parameters)
		{
			var methodBuilder = tb.DefineMethod(methodName, MethodAttributes.Public, returnType, parameters);
			var ilGenerator = methodBuilder.GetILGenerator();

			if (returnType != typeof(void))
			{
				ilGenerator.Emit(OpCodes.Ldc_I4_1);
			}

			ilGenerator.Emit(OpCodes.Ret);
		}

		private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
		{
			var fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

			var propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
			var getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
			var getIl = getPropMthdBldr.GetILGenerator();

			getIl.Emit(OpCodes.Ldarg_0);
			getIl.Emit(OpCodes.Ldfld, fieldBuilder);
			getIl.Emit(OpCodes.Ret);

			var setPropMthdBldr =
				tb.DefineMethod("set_" + propertyName,
				  MethodAttributes.Public |
				  MethodAttributes.SpecialName |
				  MethodAttributes.HideBySig,
				  null, new[] { propertyType });

			var setIl = setPropMthdBldr.GetILGenerator();
			var modifyProperty = setIl.DefineLabel();
			var exitSet = setIl.DefineLabel();

			setIl.MarkLabel(modifyProperty);
			setIl.Emit(OpCodes.Ldarg_0);
			setIl.Emit(OpCodes.Ldarg_1);
			setIl.Emit(OpCodes.Stfld, fieldBuilder);

			setIl.Emit(OpCodes.Nop);
			setIl.MarkLabel(exitSet);
			setIl.Emit(OpCodes.Ret);

			propertyBuilder.SetGetMethod(getPropMthdBldr);
			propertyBuilder.SetSetMethod(setPropMthdBldr);
		}
	}
}
