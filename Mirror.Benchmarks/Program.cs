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
		private Func<object, object> CachedFuncs;
		private MethodInfo CachedMethodInfo;
		private Type _type;

		private object _objectInstance;
		private IReflectionBenchmarkTestClass _interfaceInstance;
		private ReflectionBenchmarkTestClass _instance;

		public ReflectionBenchmark()
		{
			_instance = new ReflectionBenchmarkTestClass();
			_interfaceInstance = _instance;
			_objectInstance = _instance;
			_type = _instance.GetType();
			CachedMethodInfo = _type.GetRuntimeMethod(nameof(ReflectionBenchmarkTestClass.TestMethod), new Type[0]);
			CachedFuncs = DelegateFactory.Create(_type, nameof(ReflectionBenchmarkTestClass.TestMethod));
		}

		[Benchmark]
		public Func<object, object> CreateDelegate()
		{
			return DelegateFactory.Create(_type, nameof(ReflectionBenchmarkTestClass.TestMethod));
		}

		[Benchmark]
		public object MethodInfoInvoke()
		{
			var methodInfo = _type.GetRuntimeMethod(nameof(ReflectionBenchmarkTestClass.TestMethod), new Type[0]);
			return methodInfo.Invoke(_objectInstance, new object[0]);
		}

		[Benchmark]
		public object CachedMethodInfoInvoke()
		{
			return CachedMethodInfo.Invoke(_objectInstance, new object[0]);
		}

		[Benchmark]
		public object DelegateFactoryInvoke()
		{
			return CachedFuncs.Invoke(_objectInstance);
		}

		[Benchmark]
		public void InterfaceCall()
		{
			_interfaceInstance.TestMethod();
		}

		[Benchmark]
		public void DirectCall()
		{
			_instance.TestMethod();
		}
	}

	public interface IReflectionBenchmarkTestClass
	{
		void TestMethod();
	}

	public class ReflectionBenchmarkTestClass : IReflectionBenchmarkTestClass
	{
		public void TestMethod()
		{

		}
	}
}
