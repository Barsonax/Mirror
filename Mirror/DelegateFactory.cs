using System;
using System.Reflection;

namespace Mirror
{
	public class DelegateFactory
	{
	  
		public static Func<object, object> Create(Type type, string methodName,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters);
		}

		internal static Func<object, object> CreateDelegateHelper<TObj, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func< TRet>)Delegate.CreateDelegate(typeof(Func<TRet>), method);
					return (target) => func();
				}
				else
				{
					var func = (Func<TObj, TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TRet>), method);
					return (target) => func((TObj)target);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action)Delegate.CreateDelegate(typeof(Action), method);
				return (target) =>
				{
					func();
					return null;
				};
			}
			else
			{
				var func = (Action<TObj>)Delegate.CreateDelegate(typeof(Action<TObj>), method);
				return (target) =>
				{
					func((TObj)target);
					return null;
				};
			}
		}  
		public static Func<object, object, object> Create(Type type, string methodName, Type parameter0,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0);
		}

		internal static Func<object, object, object> CreateDelegateHelper<TObj, TP0, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TRet>), method);
					return (target, p0) => func( (TP0)p0);
				}
				else
				{
					var func = (Func<TObj, TP0,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TRet>), method);
					return (target, p0) => func((TObj)target, (TP0)p0);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0>)Delegate.CreateDelegate(typeof(Action<TP0>), method);
				return (target, p0) =>
				{
					func((TP0)p0);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0>)Delegate.CreateDelegate(typeof(Action<TObj, TP0>), method);
				return (target, p0) =>
				{
					func((TObj)target, (TP0)p0);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1);
		}

		internal static Func<object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TRet>), method);
					return (target, p0, p1) => func( (TP0)p0, (TP1)p1);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TRet>), method);
					return (target, p0, p1) => func((TObj)target, (TP0)p0, (TP1)p1);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1>)Delegate.CreateDelegate(typeof(Action<TP0, TP1>), method);
				return (target, p0, p1) =>
				{
					func((TP0)p0, (TP1)p1);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1>), method);
				return (target, p0, p1) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2);
		}

		internal static Func<object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TRet>), method);
					return (target, p0, p1, p2) => func( (TP0)p0, (TP1)p1, (TP2)p2);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TRet>), method);
					return (target, p0, p1, p2) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2>), method);
				return (target, p0, p1, p2) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2>), method);
				return (target, p0, p1, p2) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3);
		}

		internal static Func<object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TRet>), method);
					return (target, p0, p1, p2, p3) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TRet>), method);
					return (target, p0, p1, p2, p3) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3>), method);
				return (target, p0, p1, p2, p3) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3>), method);
				return (target, p0, p1, p2, p3) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4);
		}

		internal static Func<object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TRet>), method);
					return (target, p0, p1, p2, p3, p4) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TRet>), method);
					return (target, p0, p1, p2, p3, p4) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4>), method);
				return (target, p0, p1, p2, p3, p4) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4>), method);
				return (target, p0, p1, p2, p3, p4) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5);
		}

		internal static Func<object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5>), method);
				return (target, p0, p1, p2, p3, p4, p5) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5>), method);
				return (target, p0, p1, p2, p3, p4, p5) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
		}

		internal static Func<object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9, Type parameter10,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9, Type parameter10, Type parameter11,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9, Type parameter10, Type parameter11, Type parameter12,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9, Type parameter10, Type parameter11, Type parameter12, Type parameter13,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13);
					return null;
				};
			}
		}  
		public static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> Create(Type type, string methodName, Type parameter0, Type parameter1, Type parameter2, Type parameter3, Type parameter4, Type parameter5, Type parameter6, Type parameter7, Type parameter8, Type parameter9, Type parameter10, Type parameter11, Type parameter12, Type parameter13, Type parameter14,  Type[] genericTypeParameters = null)
		{
			return (Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>)DelegateFactoryUtils.CreateDelegate(type, methodName, genericTypeParameters, parameter0, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, parameter7, parameter8, parameter9, parameter10, parameter11, parameter12, parameter13, parameter14);
		}

		internal static Func<object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object> CreateDelegateHelper<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TRet>(MethodInfo method)
		{
			if (typeof(TRet) != typeof(DelegateFactoryUtils.NoReturn))
			{
				if (method.IsStatic)
				{
					var func = (Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TRet>)Delegate.CreateDelegate(typeof(Func<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) => func( (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13, (TP14)p14);
				}
				else
				{
					var func = (Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14,TRet>)Delegate.CreateDelegate(typeof(Func<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14, TRet>), method);
					return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) => func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13, (TP14)p14);
				}
			}
			if (method.IsStatic)
			{
				var func = (Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14>)Delegate.CreateDelegate(typeof(Action<TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) =>
				{
					func((TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13, (TP14)p14);
					return null;
				};
			}
			else
			{
				var func = (Action<TObj,TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14>)Delegate.CreateDelegate(typeof(Action<TObj, TP0, TP1, TP2, TP3, TP4, TP5, TP6, TP7, TP8, TP9, TP10, TP11, TP12, TP13, TP14>), method);
				return (target, p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14) =>
				{
					func((TObj)target, (TP0)p0, (TP1)p1, (TP2)p2, (TP3)p3, (TP4)p4, (TP5)p5, (TP6)p6, (TP7)p7, (TP8)p8, (TP9)p9, (TP10)p10, (TP11)p11, (TP12)p12, (TP13)p13, (TP14)p14);
					return null;
				};
			}
		}  
	}
}