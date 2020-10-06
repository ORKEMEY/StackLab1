using System;
using Stack;
//using System.Collections.Generic;


namespace StackTesting
{
	class Program
	{
		static void Main(string[] args)
		{

			//System.Collections.Generic.Stack<int> stack = new System.Collections.Generic.Stack<int>();
			Stack<int> stack = new Stack<int>(2);

			stack.Push(1);
			stack.Push(2);
			stack.Push(3);
			stack.Push(4);
			stack.Push(5);

			foreach (int i in stack)
			{
				Console.WriteLine(i);
			}

			stack.Pop();
			stack.Pop();

			stack.Push(6);
			stack.Push(7);
			//============================================
			Console.WriteLine();
			Console.WriteLine(stack.ToString());
			//============================================
			Console.WriteLine();
			Console.WriteLine("Stack cantains 6: " + stack.Contains(6));

			//============================================
			int[] arr = new int[stack.Count];
			stack.CopyTo(arr);
			Console.WriteLine("\nCopied to array stack: ");
			foreach (int i in arr)
			{
				Console.Write(i + ", ");
			}

			//============================================
			Stack<int> stack2 = stack.Clone() as Stack<int>;
			Console.WriteLine("\nCloned stack: ");
			Console.WriteLine(stack2.ToString());

			//============================================

			stack2.StackCleared += () => Console.WriteLine("\nStack2 was cleared");

			stack2.Clear();
			Console.WriteLine("\nCleared stack: ");
			Console.WriteLine(stack2.ToString());

			Console.Read();
		}

	}
}
