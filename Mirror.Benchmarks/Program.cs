using System;
using System.Collections.Generic;
using System.Reflection;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Mirror.Benchmarks
{
	class Program
	{
		static void Main(string[] args)
		{
			var summary = BenchmarkRunner.Run<ReflectionBenchmark>();
			Console.ReadKey();
		}
	}

	[ClrJob]
	public class ReflectionBenchmark
	{
		private Dictionary<Type, Func<object, object>> CachedFuncs = new Dictionary<Type, Func<object, object>>();
		private Dictionary<Type, MethodInfo> CachedMethodInfo = new Dictionary<Type, MethodInfo>();
		private Type _type;

		public ReflectionBenchmark()
		{
			_type = this.GetType();
			var methodInfo = _type.GetRuntimeMethod(nameof(TestMethod), new Type[0]);
			CachedMethodInfo.Add(_type, methodInfo);
			CachedFuncs.Add(_type, DelegateFactory.Create(_type, nameof(TestMethod)));
		}


		[Benchmark]
		public object MethodInfoInvoke()
		{
			var methodInfo = _type.GetRuntimeMethod(nameof(TestMethod), new Type[0]);
			return methodInfo.Invoke(this, new object[0]);
		}

		[Benchmark]
		public object CachedMethodInfoInvoke()
		{
			return CachedMethodInfo[_type].Invoke(this, new object[0]);
		}

		[Benchmark]
		public object DelegateFactoryInvoke()
		{
			return CachedFuncs[_type].Invoke(this);
		}

		[Benchmark]
		public Func<object, object> CreateDelegate()
		{
			return DelegateFactory.Create(_type, nameof(TestMethod));
		}


		public void TestMethod()
		{

		}
	}
}
