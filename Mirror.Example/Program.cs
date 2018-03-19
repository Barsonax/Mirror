using System;
using System.Collections.Generic;

namespace Mirror.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			var list = new List<object> { new Foo(), new Foo2() };
			foreach (var foo in list)
			{
				// Normally you would store these actions and funcs in a dictionary so you can reuse them for future calls easily.
				var type = foo.GetType();
				var action = CachedReflection.CreateAction<int>(type, nameof(IFoo.DoSomething));
				var action2 = CachedReflection.CreateAction(type, nameof(IFoo.DoSomething));
				var action3 = CachedReflection.CreateFunc<int>(type, nameof(IFoo.GetRandomNumber));
				var action4 = CachedReflection.CreateFunc<int, int>(type, nameof(IFoo.GetRandomNumber));

				action.Invoke(foo, 4);
				action2.Invoke(foo);
				action3.Invoke(foo);
				action4.Invoke(foo, 7);
			}

			Console.ReadKey();
		}
	}

	public interface IFoo
	{
		int GetRandomNumber();

		void DoSomething();
		void DoSomething(int i);
	}

	public class Foo : IFoo
	{
		private readonly Random _random = new Random();
		public int GetRandomNumber()
		{
			return _random.Next();
		}

		public int GetRandomNumber(int i)
		{
			return _random.Next(i);
		}

		public void DoSomething()
		{
			Console.WriteLine(this.ToString());
		}

		public void DoSomething(int i)
		{
			Console.WriteLine(this.ToString());
		}
	}

	public class Foo2 : IFoo
	{
		private readonly Random _random = new Random();
		public int GetRandomNumber()
		{
			return _random.Next();
		}

		public void DoSomething()
		{
			Console.WriteLine(this.ToString());
		}

		public void DoSomething(int i)
		{
			Console.WriteLine(this.ToString());
		}

		public int GetRandomNumber(int i)
		{
			return _random.Next(i);
		}
	}
}
