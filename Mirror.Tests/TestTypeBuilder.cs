using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mirror.Tests
{
	public class TestTypeBuilder
	{
		readonly CodeDomProvider _codeCompiler = CodeDomProvider.CreateProvider("CSharp");

		readonly CompilerParameters objCompilerParameters = new CompilerParameters();

		public Type CreateClass(string typeName, IEnumerable<MethodDescriptor> methods)
		{
			objCompilerParameters.ReferencedAssemblies.Add(Assembly.GetCallingAssembly().Location);
			var stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("using System;");
			stringBuilder.AppendLine("using Mirror.Tests;");
			stringBuilder.AppendLine($"public class {typeName}");
			stringBuilder.AppendLine("{");
			foreach (var methodDescriptor in methods)
			{
				CreateMethod(stringBuilder, methodDescriptor);
			}
			stringBuilder.AppendLine("}");
			var source = stringBuilder.ToString();
			var cr = _codeCompiler.CompileAssemblyFromSource(objCompilerParameters, source);
			return cr.CompiledAssembly.GetType(typeName);
		}

		private void CreateMethod(StringBuilder stringBuilder, MethodDescriptor methodDescriptor)
		{
			var parameters = string.Join(", ", methodDescriptor.Parameters.Select((x, i) => $"p{i}"));
			var genericParameters = string.Join(", ", methodDescriptor.GenericParameters.Select((x, i) => $"T{i}"));
			var genericTypeParameters = string.Join(", ", methodDescriptor.GenericParameters.Select((x, i) => $"typeof({x.Name})"));

			if (methodDescriptor.GenericParameters.Length > 0)
			{
				stringBuilder.Append($"public MethodCallInfo {methodDescriptor.MethodName}<{genericParameters}>(");
			}
			else
			{
				stringBuilder.Append($"public MethodCallInfo {methodDescriptor.MethodName}(");
			}

			stringBuilder.Append(string.Join(", ", methodDescriptor.Parameters.Select((x, i) => $"{x.Name} p{i}")));
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine("{");

			if (methodDescriptor.GenericParameters.Length > 0)
			{
				stringBuilder.AppendLine($"return new MethodCallInfo(new object[] {{{parameters}}}, \"TestMethod\", new[] {{{genericTypeParameters}}});");
			}
			else
			{
				stringBuilder.AppendLine($"return new MethodCallInfo(new object[] {{{parameters}}}, \"TestMethod\");");
			}

			stringBuilder.AppendLine("}");
		}
	}

	public class MethodDescriptor
	{
		public string MethodName { get; }
		public Type[] Parameters { get; }
		public Type[] GenericParameters { get; }

		public MethodDescriptor(string methodName, Type[] parameters, Type[] genericParameters = null)
		{
			MethodName = methodName;
			Parameters = parameters;
			GenericParameters = genericParameters ?? new Type[0];
		}
	}
}
