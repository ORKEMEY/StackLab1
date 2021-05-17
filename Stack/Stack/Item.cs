using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NUnitTestStack")]
namespace Stack
{
	internal class Item<T> : IDisposable
	{
		private int _Index { get; set; }
		public int Index
		{
			get
			{
				return _Index;
			}
			set
			{
				if (Prev != null) _Index = Prev.Index + 1;
				else _Index = 0;
			}
		}

		public T Object { get; set; }

		internal bool IsFilled;

		public Item<T> Prev { get; set; }

		internal Item(Item<T> head)
		{
			Object = default;
			Prev = head;
			IsFilled = false;
			Index = 0;
		}

		internal Item(Item<T> head, T obj)
		{
			Object = obj;
			Prev = head;
			IsFilled = true;
			Index = 0;
		}

		public void Dispose()
		{
			Object = default;
			Prev = null;
			IsFilled = default;
			_Index = default;
		}

	}
}
