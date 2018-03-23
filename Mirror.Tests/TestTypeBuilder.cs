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
			objCompilerParameters.GenerateInMemory = true;
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
			var returnType = methodDescriptor.ReturnsValue ? "MethodCallInfo" : "void";
			if (methodDescriptor.GenericParameters.Length > 0)
			{
				stringBuilder.Append($"public {returnType} {methodDescriptor.MethodName}<{genericParameters}>(");
			}
			else
			{
				stringBuilder.Append($"public {returnType} {methodDescriptor.MethodName}(");
			}

			stringBuilder.Append(string.Join(", ", methodDescriptor.Parameters.Select((x, i) => $"{x.Name} p{i}")));
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine("{");

			if (methodDescriptor.ReturnsValue)
			{
				if (methodDescriptor.GenericParameters.Length > 0)
				{
					stringBuilder.AppendLine(
						$"return new MethodCallInfo(new object[] {{{parameters}}}, \"TestMethod\", new[] {{{genericTypeParameters}}});");
				}
				else
				{
					stringBuilder.AppendLine($"return new MethodCallInfo(new object[] {{{parameters}}}, \"TestMethod\");");
				}
			}
			else
			{
				stringBuilder.AppendLine("return;");
			}

			stringBuilder.AppendLine("}");
		}
	}

	public class MethodDescriptor
	{
		public string MethodName { get; }
		public Type[] Parameters { get; }
		public Type[] GenericParameters { get; }
		public bool ReturnsValue { get; }

		public MethodDescriptor(string methodName, Type[] parameters, Type[] genericParameters = null, bool returnsValue = true)
		{
			MethodName = methodName;
			Parameters = parameters;
			GenericParameters = genericParameters ?? new Type[0];
			ReturnsValue = returnsValue;
		}
	}
}
