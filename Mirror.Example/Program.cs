using System;
using System.Collections;
using System.Collections.Generic;

namespace Mirror.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			IEnumerable hashset = new HashSet<object> { new Foo(), new Foo2() };
			var type = hashset.GetType();
			var add = DelegateFactory.GetDelegate(type, "Add", type.GenericTypeArguments[0]);
			add.Invoke(hashset, new Foo());

			Console.ReadKey();
		}
	}

	public class Foo { }

	public class Foo2 { }
}
