using System;
using System.Linq;
using System.Reflection;

namespace Mirror
{
	public static class DelegateFactoryUtils
	{
		private static MethodInfo[] _genericHelpers;
		static DelegateFactoryUtils()
		{
			var foundMethods = typeof(DelegateFactory).GetRuntimeMethods().Where(m => m.Name == nameof(DelegateFactory.CreateDelegateHelper)).ToArray();
			_genericHelpers = new MethodInfo[foundMethods.Length + 2];
			for (int i = 0; i < foundMethods.Length; i++)
			{
				_genericHelpers[foundMethods[i].GetGenericArguments().Length] = foundMethods[i];
			}
		}

		public static object CreateDelegate(Type type, string methodName, Type[] genericMethodParameters = null, params Type[] parameterTypes)
		{
			MethodInfo methodInfo;
			if (genericMethodParameters != null && genericMethodParameters.Length > 0)
			{
				MethodInfo genericMethod = null;
				foreach (var m in type.GetRuntimeMethods())
				{
					if (m.Name == methodName && m.GetGenericArguments().Length == genericMethodParameters.Length)
					{
						genericMethod = m;
						break;
					}
				}

				methodInfo = genericMethod.MakeGenericMethod(genericMethodParameters);
			}
			else
			{
				methodInfo = null;
				foreach (var m in type.GetRuntimeMethods())
				{
					if (m.Name == methodName && !m.IsGenericMethod)
					{
						methodInfo = m;
						break;
					}
				}
			}

			if (methodInfo == null) throw new ArgumentException($"Could not find a method on type {type} with name {methodName} and parameter types {string.Join(", ", parameterTypes.Select(x => x.Name))}");
			return CreateDelegate(methodInfo);
		}

		public static object CreateDelegate(MethodInfo method)
		{
			var genericMethodParameters = GetMethodParameters(method);
			var genericHelper = _genericHelpers[genericMethodParameters.Length];
			//var genericHelper = typeof(DelegateFactory).GetRuntimeMethods().Where(m => m.Name == nameof(CreateDelegateHelper)).FirstOrDefault(m => m.GetGenericArguments().Length == genericMethodParameters.Length);

			if (genericHelper == null) throw new NotImplementedException($"No {nameof(DelegateFactory.CreateDelegateHelper)} found with {genericMethodParameters.Length} generic parameters");

			var constructedHelper = genericHelper.MakeGenericMethod(genericMethodParameters);
			return constructedHelper.Invoke(null, new object[] { method });
		}

		private static Type[] GetMethodParameters(MethodInfo method)
		{
			var parameters = method.GetParameters();
			var parameterCount = 2 + parameters.Length;
			var genericMethodParameters = new Type[parameterCount];
			genericMethodParameters[0] = method.DeclaringType;
			for (var i = 0; i < parameters.Length; i++)
			{
				genericMethodParameters[i + 1] = parameters[i].ParameterType;
			}
			genericMethodParameters[genericMethodParameters.Length - 1] = method.ReturnType != typeof(void) ? method.ReturnType : typeof(NoReturn);
			return genericMethodParameters;
		}

		internal class NoReturn { }
	}
}