using System;
using System.Linq;
using System.Reflection;

namespace Mirror
{
	/// <summary>
	/// Does the heavy lifting.
	/// Currently only supports a limited set of method parameters. Once you understand the concept though it should be easy to expand this.
	/// </summary>
	public class DelegateFactory
	{
		public static Func<object, object, object, object, object> GetDelegate(Type type, string methodName, Type parameter1, Type parameter2, Type parameter3)
		{
			return (Func<object, object, object, object, object>)CreateDelegate(type, methodName, parameter1, parameter2);
		}

		public static Func<object, object, object, object> GetDelegate(Type type, string methodName, Type parameter1, Type parameter2)
		{
			return (Func<object, object, object, object>)CreateDelegate(type, methodName, parameter1, parameter2);
		}

		public static Func<object, object, object> GetDelegate(Type type, string methodName, Type parameter1)
		{
			return (Func<object, object, object>)CreateDelegate(type, methodName, parameter1);
		}

		public static Func<object, object> GetDelegate(Type type, string methodName)
		{
			return (Func<object, object>)CreateDelegate(type, methodName);
		}

		private static object CreateDelegate(Type type, string methodName, params Type[] parameterTypes)
		{
			var methodInfo = type.GetRuntimeMethod(methodName, parameterTypes);
			return CreateDelegate(methodInfo);
		}

		private static object CreateDelegate(MethodInfo method)
		{
			var genericMethodParameters = GetMethodParameters(method);
			var genericHelper = (from m in typeof(DelegateFactory).GetRuntimeMethods()
								 where m.Name == nameof(CreateFuncHelper)
								 where m.GetGenericArguments().Length == genericMethodParameters.Length
								 select m).First();

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

		private class NoReturn { }

		private static Func<object, object, object> CreateFuncHelper<TObj, TP1, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TRet>), method);
				return (target, p1) => func((TObj)target, (TP1)p1);
			}
			else
			{
				var func = (Action<TObj, TP1>)Delegate.CreateDelegate(typeof(Action<TObj, TP1>), method);
				return (target, p1) =>
				{
					func((TObj)target, (TP1)p1);
					return null;
				};
			}
		}

		private static Func<object, object> CreateFuncHelper<TObj, TRet>(MethodInfo method)
			where TObj : class
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TRet>), method);
				return target => func(target as TObj);
			}
			else
			{
				var func = (Action<TObj>)Delegate.CreateDelegate(typeof(Action<TObj>), method);
				return target =>
				{
					func(target as TObj);
					return default(TRet);
				};
			}
		}
	}


}
