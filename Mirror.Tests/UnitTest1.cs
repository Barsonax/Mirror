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
				var type = typeof(TestClass);
				var methods = type.GetRuntimeMethods().Where(x => x.Module == type.Module).ToArray();
				var cachedReflectionMethods = typeof(DelegateFactory).GetRuntimeMethods().Where(x => x.Name == nameof(DelegateFactory.GetDelegate))
					.ToDictionary(x => x.GetParameters().Length - 2);
				foreach (var methodInfo in methods)
				{
					yield return new object[] { cachedReflectionMethods[methodInfo.GetParameters().Length], methodInfo };
				}
			}
		}

		[Theory]
		[MemberData(nameof(TestCases))]
		public void GetDelegate(MethodInfo cachedReflectionMethod, MethodInfo methodInfo)
		{
			var obj = new TestClass();

			var parameterInfos = methodInfo.GetParameters();
			var parameters = new object[parameterInfos.Length];
			for (var i = 0; i < parameterInfos.Length; i++)
			{
				parameters[i] = Activator.CreateInstance(parameterInfos[i].ParameterType);
			}

			var reflectionResult = methodInfo.Invoke(obj, parameters);

			var actionParameters = new object[parameters.Length + 2];
			actionParameters[0] = typeof(TestClass);
			actionParameters[1] = methodInfo.Name;
			for (int i = 0; i < parameterInfos.Length; i++)
			{
				actionParameters[i + 2] = parameterInfos[i].ParameterType;
			}
			var action = cachedReflectionMethod.Invoke(obj, actionParameters);
			var actionMethodInfo = action.GetType().GetRuntimeMethods().First(x => x.Name == nameof(Func<int>.Invoke));
			var cachedReflectionResult = actionMethodInfo.Invoke(action, new[] { obj }.Concat(parameters).ToArray());

			Assert.Equal(reflectionResult, cachedReflectionResult);
		}

		public class TestClass
		{
			public int Return1Parameter(int i1) => 1;
			public int ReturnNoParameters() => 1;
			public void VoidNoParameters() { }
			public void Void1Parameter(int i1) { }
		}
	}
}
