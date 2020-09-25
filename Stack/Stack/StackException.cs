using System;
using System.Collections.Generic;
using System.Text;

namespace Stack
{
	public class StackException : Exception
	{

		public static string IndexOutOfRange = "Index is out of range";
		public static string ReferringToEmptyStack = "Referring to empty stack";
		public static string NegativeCapacity = "Negative capacity";

		internal StackException(string message) : base(message)
		{
		}
	}
}
