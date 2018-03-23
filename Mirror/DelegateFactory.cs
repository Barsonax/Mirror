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
		private static MethodInfo[] _genericHelpers;
		static DelegateFactory()
		{
			var foundMethods = typeof(DelegateFactory).GetRuntimeMethods().Where(m => m.Name == nameof(CreateDelegateHelper)).ToArray();
			_genericHelpers = new MethodInfo[foundMethods.Length + 2];
			for (int i = 0; i < foundMethods.Length; i++)
			{
				_genericHelpers[foundMethods[i].GetGenericArguments().Length] = foundMethods[i];
			}
		}

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

		/// <summary>
		/// Returns a <see cref="Func{Instance,Return}"/> that can be used to call the method/>
		/// </summary>
		/// <param name="type">The type that has the method</param>
		/// <param name="methodName">The name of the method</param>
		/// <param name="genericTypeParameters">Generic parameters for the method</param>
		/// <returns></returns>
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

		private static object CreateDelegate(MethodInfo method)
		{
			var genericMethodParameters = GetMethodParameters(method);
			var genericHelper = _genericHelpers[genericMethodParameters.Length];
			//var genericHelper = typeof(DelegateFactory).GetRuntimeMethods().Where(m => m.Name == nameof(CreateDelegateHelper)).FirstOrDefault(m => m.GetGenericArguments().Length == genericMethodParameters.Length);

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