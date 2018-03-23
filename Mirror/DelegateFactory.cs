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
		public static Func<object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
		}

		public static Func<object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1, parameter2, parameter3, parameter4, parameter5);
		}

		public static Func<object, object, object, object, object, object> Create(Type type, string methodName, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1, parameter2, parameter3, parameter4);
		}

		public static Func<object, object, object, object, object> Create(Type type, string methodName, Type parameter1, Type parameter2, Type parameter3, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1, parameter2, parameter3);
		}

		public static Func<object, object, object, object> Create(Type type, string methodName, Type parameter1, Type parameter2, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1, parameter2);
		}

		public static Func<object, object, object> Create(Type type, string methodName, Type parameter1, Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object>)CreateDelegate(type, methodName, genericTypeParameters, parameter1);
		}

		public static Func<object, object> Create(Type type, string methodName, Type[] genericTypeParameters = null)
		{
			return (Func<object, object>)CreateDelegate(type, methodName, genericTypeParameters);
		}

		private static Func<object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP1, TP2, TP3, TP4, TP5, TP6, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TP2, TP3, TP4, TP5, TP6, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TP2, TP3, TP4, TP5, TP6, TRet>), method);
				return (target, p1, p2, p3, p4, p5, p6) => func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
			}
			else
			{
				var func = (Action<TObj, TP1, TP2, TP3, TP4, TP5, TP6>)Delegate.CreateDelegate(typeof(Action<TObj, TP1, TP2, TP3, TP4, TP5, TP6>), method);
				return (target, p1, p2, p3, p4, p5, p6) =>
				{
					func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
					return null;
				};
			}
		}

		private static Func<object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP1, TP2, TP3, TP4, TP5, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TP2, TP3, TP4, TP5, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TP2, TP3, TP4, TP5, TRet>), method);
				return (target, p1, p2, p3, p4, p5) => func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
			}
			else
			{
				var func = (Action<TObj, TP1, TP2, TP3, TP4, TP5>)Delegate.CreateDelegate(typeof(Action<TObj, TP1, TP2, TP3, TP4, TP5>), method);
				return (target, p1, p2, p3, p4, p5) =>
				{
					func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
					return null;
				};
			}
		}

		private static Func<object, object, object, object, object, object> CreateDelegateHelper<TObj, TP1, TP2, TP3, TP4, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TP2, TP3, TP4, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TP2, TP3, TP4, TRet>), method);
				return (target, p1, p2, p3, p4) => func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
			}
			else
			{
				var func = (Action<TObj, TP1, TP2, TP3, TP4>)Delegate.CreateDelegate(typeof(Action<TObj, TP1, TP2, TP3, TP4>), method);
				return (target, p1, p2, p3, p4) =>
				{
					func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
					return null;
				};
			}
		}

		private static Func<object, object, object, object, object> CreateDelegateHelper<TObj, TP1, TP2, TP3, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TP2, TP3, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TP2, TP3, TRet>), method);
				return (target, p1, p2, p3) => func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3);
			}
			else
			{
				var func = (Action<TObj, TP1, TP2, TP3>)Delegate.CreateDelegate(typeof(Action<TObj, TP1, TP2, TP3>), method);
				return (target, p1, p2, p3) =>
				{
					func((TObj)target, (TP1)p1, (TP2)p2, (TP3)p3);
					return null;
				};
			}
		}

		private static Func<object, object, object, object> CreateDelegateHelper<TObj, TP1, TP2, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(NoReturn))
			{
				var func = (Func<TObj, TP1, TP2, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP1, TP2, TRet>), method);
				return (target, p1, p2) => func((TObj)target, (TP1)p1, (TP2)p2);
			}
			else
			{
				var func = (Action<TObj, TP1, TP2>)Delegate.CreateDelegate(typeof(Action<TObj, TP1, TP2>), method);
				return (target, p1, p2) =>
				{
					func((TObj)target, (TP1)p1, (TP2)p2);
					return null;
				};
			}
		}

		private static Func<object, object, object> CreateDelegateHelper<TObj, TP1, TRet>(MethodInfo method)
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

		private static Func<object, object> CreateDelegateHelper<TObj, TRet>(MethodInfo method)
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

		private static object CreateDelegate(Type type, string methodName, Type[] genericMethodParameters = null, params Type[] parameterTypes)
		{
			MethodInfo methodInfo;
			if (genericMethodParameters != null && genericMethodParameters.Length > 0)
			{
				var genericMethod = type.GetRuntimeMethods().Where(m => m.Name == methodName).FirstOrDefault(m => m.GetGenericArguments().Length == genericMethodParameters.Length);
				methodInfo = genericMethod.MakeGenericMethod(genericMethodParameters);
			}
			else
			{
				methodInfo = type.GetRuntimeMethods().Where(m => m.Name == methodName).FirstOrDefault(m => m.GetGenericArguments().Length == 0);
			}

			if (methodInfo == null) throw new ArgumentException($"Could not find a method on type {type} with name {methodName} and parameter types {string.Join(", ", parameterTypes.Select(x => x.Name))}");
			return CreateDelegate(methodInfo);
		}

		private static object CreateDelegate(MethodInfo method)
		{
			var genericMethodParameters = GetMethodParameters(method);
			var genericHelper = typeof(DelegateFactory).GetRuntimeMethods().Where(m => m.Name == nameof(CreateDelegateHelper)).FirstOrDefault(m => m.GetGenericArguments().Length == genericMethodParameters.Length);

			if (genericHelper == null) throw new NotImplementedException($"No {nameof(CreateDelegateHelper)} found with {genericMethodParameters.Length} generic parameters");

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
	}
}