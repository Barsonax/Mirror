using System;

namespace Mirror.Tests
{
	public class MethodCallInfo
	{
		public object[] ParameterValues { get; }
		public string MethodName { get; }
		public Type[] GenericMethodParameters { get; }

		public MethodCallInfo(object[] parameterValues, string methodName, Type[] genericMethodParameters = null)
		{
			ParameterValues = parameterValues;
			MethodName = methodName;
			GenericMethodParameters = genericMethodParameters ?? new Type[0];
		}
	}
}
