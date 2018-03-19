using System;
using System.Linq;
using System.Reflection;

namespace Mirror
{
	public class CachedReflection
	{
		public static Action<object, TParameter1> CreateAction<TParameter1>(Type type, string methodName)
		{
			return (Action<object, TParameter1>)CreateDelegate<TParameter1>(type, methodName);
		}

		public static Action<object> CreateAction(Type type, string methodName)
		{
			return (Action<object>)CreateDelegate(type, methodName);
		}

		public static Func<object, TParameter1, TReturn> CreateFunc<TParameter1, TReturn>(Type type, string methodName)
		{
			return (Func<object, TParameter1, TReturn>)CreateDelegate<TParameter1>(type, methodName);
		}

		public static Func<object, TReturn> CreateFunc<TReturn>(Type type, string methodName)
		{
			return (Func<object, TReturn>)CreateDelegate(type, methodName);
		}

		private static object CreateDelegate(Type type, string methodName)
		{
			var methodInfo = type.GetRuntimeMethod(methodName, new Type[] { });
			return CreateDelegate(methodInfo);
		}

		private static object CreateDelegate<TParameter1>(Type type, string methodName)
		{
			var methodInfo = type.GetRuntimeMethod(methodName, new[] { typeof(TParameter1) });
			return CreateDelegate(methodInfo);
		}

		private static object CreateDelegate(MethodInfo method)
		{
			var genericMethodParameters = GetMethodParameters(method);
			var methodName = method.ReturnType == typeof(void) ? nameof(CreateActionHelper) : nameof(CreateFuncHelper);
			var genericHelper = (from m in typeof(CachedReflection).GetRuntimeMethods()
								 where m.Name == methodName
								 where m.GetGenericArguments().Length == genericMethodParameters.Length
								 select m).First();

			var constructedHelper = genericHelper.MakeGenericMethod(genericMethodParameters);
			return constructedHelper.Invoke(null, new object[] { method });
		}

		private static Type[] GetMethodParameters(MethodInfo method)
		{
			var parameters = method.GetParameters();
			var parameterCount = 1 + parameters.Length;
			if (method.ReturnType != typeof(void)) parameterCount++;
			var genericMethodParameters = new Type[parameterCount];
			genericMethodParameters[0] = method.DeclaringType;
			for (var i = 0; i < parameters.Length; i++)
			{
				genericMethodParameters[i + 1] = parameters[i].ParameterType;
			}

			if (method.ReturnType != typeof(void)) genericMethodParameters[genericMethodParameters.Length - 1] = method.ReturnType;
			return genericMethodParameters;
		}

		private static Func<object, TParameter1, TReturn> CreateFuncHelper<TActual, TParameter1, TReturn>(MethodInfo method)
			where TActual : class
		{
			var func = (Func<TActual, TParameter1, TReturn>)Delegate.CreateDelegate(typeof(Func<TActual, TParameter1, TReturn>), method);
			return (target, p1) => func(target as TActual, p1);
		}

		private static Func<object, TReturn> CreateFuncHelper<TActual, TReturn>(MethodInfo method)
			where TActual : class
		{
			var func = (Func<TActual, TReturn>)Delegate.CreateDelegate(typeof(Func<TActual, TReturn>), method);
			return target => func(target as TActual);
		}

		private static Action<object> CreateActionHelper<TActual>(MethodInfo method)
			where TActual : class
		{
			var del = (Action<TActual>)Delegate.CreateDelegate(typeof(Action<TActual>), method);
			return target => del(target as TActual);
		}

		private static Action<object, TParameter1> CreateActionHelper<TActual, TParameter1>(MethodInfo method)
			where TActual : class
		{
			var del = (Action<TActual, TParameter1>)Delegate.CreateDelegate(typeof(Action<TActual, TParameter1>), method);
			return (target, p1) => del(target as TActual, p1);
		}
	}
}
