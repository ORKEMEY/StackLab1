using System;
using System.Collections.Generic;
using System.Collections;

namespace Stack
{
	public class Stack<T> : IEnumerable<T>, IEnumerator<T>, ICloneable, ICollection
	{
		private Item<T> _Head;
		public T Head
		{
			get
			{
				if (_Head == null || _Head.IsFilled == false) throw new StackException(StackException.ReferringToEmptyStack);
				return _Head.Object;
			}
		}

		private Item<T> _CapacityHead;

		public int Capacity
		{
			get
			{
				if (_CapacityHead != null)
					return _CapacityHead.Index + 1;
				else return 0;
			}
		}
		public int Count
		{
			get
			{
				if (_Head != null)
					return _Head.Index + 1;
				else return 0;
			}
		}
		private int position { get; set; }

		public bool IsSynchronized => false;

		public object SyncRoot => new object();

		public delegate void ItemPushedDelegate(object item);
		public event ItemPushedDelegate ItemPushed;

		public delegate void ItemPopedDelegate(object item);
		public event ItemPopedDelegate ItemPoped;

		public delegate void StackClearedDelegate();
		public event StackClearedDelegate StackCleared;

		public delegate void StackClonedDelegate();
		public event StackClonedDelegate StackCloned;

		private T this[int ind]
		{
			get
			{
				return GetItemByInd(ind).Object;
			}
			set
			{
				this[ind] = value;
			}

		}

		public Stack()
		{
			_CapacityHead = _Head = null;
			position = this.Count;
		}

		public Stack(int capacity)
		{

			_CapacityHead = _Head = null;
			position = this.Count;

			if(capacity < 0) throw new StackException(StackException.NegativeCapacity);
			if (capacity > 0) _CapacityHead = new Item<T>(_CapacityHead);
			for (int count = 1; count < capacity; count++)
			{
				_CapacityHead = new Item<T>(_CapacityHead);
			}
			
		}

		public override string ToString()
		{
			string str = "";
			foreach (T i in this)
			{
				str += i.ToString() + ", ";
			}

			return str;
		}

		public void Dispose()
		{
			/*Item<T> curDis, cur = _CapacityHead;

			while(cur != null)
			{
				curDis = cur;
				cur = cur.Prev;
				curDis.Dispose();
				curDis = null;
			}*/

		}

		public IEnumerator<T> GetEnumerator()
		{
			Reset();
			return this;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public bool MoveNext()
		{
			position--;
			return (position >= 0);
		}
		public void Reset() => position = this.Count;
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}
		public T Current
		{
			get
			{
				try
				{
					return this[position];
				}
				catch (IndexOutOfRangeException)
				{
					throw new StackException(StackException.IndexOutOfRange);
				}
			}
		}

		private Item<T> GetItemByInd(int ind)
		{
			if (_Head == null || _Head.IsFilled == false) throw new StackException(StackException.ReferringToEmptyStack);
			if (ind >= Count) throw new StackException(StackException.IndexOutOfRange);

			int shift = Capacity - ind - 1;
			Item<T> cur = _CapacityHead;
			for (int count = 0; count < shift; count++)
			{
				cur = cur.Prev;
			}
			return cur;
		}

		private Item<T> GetNextEmptyItem()
		{
			if (_CapacityHead == _Head) return null;
			Item<T> cur = _CapacityHead;

			while (cur.Prev != null && cur.Prev.IsFilled == false) 
			{
				cur = cur.Prev;
			}
			return cur;
		}

		public void Push(T newObj)
		{

			Item<T> nextItem = GetNextEmptyItem();

			if(nextItem != null)
			{
				nextItem.Object = newObj;
				nextItem.IsFilled = true;
				_Head = nextItem;
			}
			else
			{
				_CapacityHead = _Head = new Item<T>(_Head, newObj);
			}
			ItemPushed?.Invoke(newObj);
		}
		public T Pop()
		{
			if(_Head == null) throw new StackException(StackException.ReferringToEmptyStack);
			_Head.IsFilled = false;
			ItemPoped?.Invoke(_Head.Object);
			_Head = _Head.Prev;
			if (_Head == null) return default;
			return Head;
		}
		public T Peek()
		{
			if (_Head == null) throw new StackException(StackException.ReferringToEmptyStack);
			return Head;
		}
		public void Clear()
		{
			foreach (T i in this)
			{
				Pop();
			}
			StackCleared?.Invoke();
		}
		public bool Contains(T itemToFind)
		{
			IEqualityComparer<T> comparer  =  EqualityComparer<T>.Default;

			foreach (T item in this)
			{
				if (comparer.Equals(item, itemToFind))
					return true;
			}
			return false;
			
		}
		public T[] ToArray()
		{
			if (_Head == null) throw new StackException(StackException.ReferringToEmptyStack);
			T[] arr = new T[this.Count];
			for(int count = 0; count < Count; count++)
			{
				arr[count] = this[count];
			}
			return arr;
		}

		public object Clone()
		{
			if (_Head == null || _Head.IsFilled == false) throw new StackException(StackException.ReferringToEmptyStack);
			Stack<T> stackCopy = new Stack<T>();
			for (int count = 0; count < Count; count++)
			{
				stackCopy.Push(this[count]);
			}
			StackCloned?.Invoke();
			return stackCopy;
		}
		public void CopyTo(Array array, int beginIndex = 0)
		{
			if (_Head == null || _Head.IsFilled == false) throw new StackException(StackException.ReferringToEmptyStack);
			if (beginIndex >= this.Count || beginIndex < 0) throw new StackException(StackException.IndexOutOfRange);
		
			for (int count = beginIndex; count < Count; count++)
			{
				array.SetValue(this[count], count - beginIndex);
			}
		}
	}

}
