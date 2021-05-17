using System;
using Stack;
using NUnit.Framework;

namespace NUnitTestStack
{
    [TestFixture]
    class StackTests
    {
        
        [TestCase(null)]
        public void Peek_refNullPushed_refNullReturend(string item)
        {
            //arrange
            Stack<string> stack = new Stack<string>();
            //act
            stack.Push(item);
            //assert
            Assert.AreEqual(item, stack.Peek());
        }
        [Test]
        public void Peek_GenTypePushed_GenTypeReturend<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(item);
            //act

            //assert
            Assert.AreEqual(item, stack.Peek());
        }
        [Test]
        public void Peek_EmptyStackPeeked_StackException()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            //act
            //assert
            Assert.Throws<StackException>(() => stack.Peek());
        }


        [Test]
        public void Constructor_IitializeWithSizeN_CapacityReturnedN([Values(0, 1)] int size)
        {
            //arrange
            //act
            Stack<int> stack = new Stack<int>(size);
            //assert
            Assert.AreEqual(size, stack.Capacity);
            Assert.AreEqual(0, stack.Count);
        }
        [Test]
        public void Constructor_IitializeWithSizeNeg_StackException()
        {
            //arrange
            int size = -1;
            //act
            //assert
            Assert.Throws<StackException>(() => new Stack<int>(size));
        }


        [TestCase(null)]
        public void Push_refNullPushed_refNullReturend(string item)
        {
            //arrange
            Stack<string> stack = new Stack<string>();
            int expectedSize = 2;
            //act
            stack.Push(item);
            stack.Push(item);
            //assert
            Assert.AreEqual(expectedSize, stack.Count);
            Assert.AreEqual(item, stack.Peek());
        }
        [Test]
        public void Push_GenTypePushed_GenTypeReturend<T>([Values("aaa", -1, 0, 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            int expectedSize = 2;
            //act
            stack.Push(item);
            stack.Push(item);
            //assert
            Assert.AreEqual(expectedSize, stack.Count);
            Assert.AreEqual(item, stack.Peek());
        }
        [Test]
        public void Push_PushItem_ItemPushedDelegateEventRaised<T>([Values("aaa", 1)] T item)
        {
            //arrange
            System.Collections.Generic.IEqualityComparer<T> comparer = System.Collections.Generic.EqualityComparer<T>.Default;
            Stack<T> stack = new Stack<T>();
            int pushId = 0;
            bool EventRaised = false;
            stack.ItemPushed += (object st, T it, int id) =>
            {
                if(stack == st && comparer.Equals(item, it) && pushId == id)
                EventRaised = true;
            };
            //act
            stack.Push(item);
            //assert
            Assert.IsTrue(EventRaised);
        }


        [Test(ExpectedResult = 10)]
        public int Pop_intPushed_intPoped()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            int expectedInt = 10;
            stack.Push(expectedInt);
            stack.Push(1);
            //act
            stack.Pop();
            //assert
            return stack.Peek();
        }
        [Test]
        public void Pop_GenTypePushed_GenTypeReturend<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            T expectedItem = default;
            int expectedSize = 1;
            stack.Push(expectedItem);
            stack.Push(item);
            //act
            stack.Pop();
            //assert
            Assert.AreEqual(expectedSize, stack.Count, "Wrong size of stack");
            Assert.AreEqual(expectedItem, stack.Peek(), "Unexpected item in stack");
        }
        [Test]
        public void Pop_PopEmptyStack_StackException()
		{
            //arrange
            Stack<string> stack = new Stack<string>();
            //act

            //assert
            Assert.Throws<StackException>(() => stack.Pop()); 
        }
        [Test]
        public void Pop_PopItem_ItemPopedDelegateEventRaised<T>([Values("aaa", 1)] T item)
        {
            //arrange
            System.Collections.Generic.IEqualityComparer<T> comparer = System.Collections.Generic.EqualityComparer<T>.Default;
            Stack<T> stack = new Stack<T>();
            int popId = 0;
            bool EventRaised = false;
            stack.ItemPoped += (object st, T it, int id) =>
            {
                if (stack == st && comparer.Equals(item, it) && popId == id)
                    EventRaised = true;
            };
            stack.Push(item);
            //act
            stack.Pop();
            //assert
            Assert.IsTrue(EventRaised);
        }

        [Test(ExpectedResult = 0)]
        public int Clear_ClearStack_StackIsEmpty()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            int item = 1;
            stack.Push(item);
            stack.Push(item);
            //act
            stack.Clear();
            //assert
            return stack.Count;
        }
        [Test]
        public void Clear_ClearStack_StackClearedDelegateEventRaised<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            bool EventRaised = false;
            stack.StackCleared += (object st) =>
            {
                if (stack == st)
                    EventRaised = true;
            };
            stack.Push(item);
            //act
            stack.Clear();
            //assert
            Assert.IsTrue(EventRaised);
        }


        [Test]
        public void Contains_ContainsCertainGenType_TrueReturned<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            bool Cont;
            stack.Push(item);
            //act
            Cont = stack.Contains(item);
            //assert
            Assert.IsTrue(Cont);
        }
        [Test]
        public void Contains_NotContainsCertainGenType_FalseReturned<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            bool Cont;
            stack.Push(item);
            //act
            Cont = stack.Contains(default);
            //assert
            Assert.IsFalse(Cont);
        }
        [Test]
        public void Contains_EmptyStack_FalseReturned()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            int item = 1;
            bool Cont;
            //act
            Cont = stack.Contains(item);
            //assert
            Assert.AreEqual(Cont, false);
        }


        [Test]
        public void ToArray_Invoke_ArrayReturned<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(item);
            stack.Push(item);
            T[] arr;
            //act
            arr = stack.ToArray();
            //assert
            CollectionAssert.AreEqual(arr, stack);
        }
        [Test]
        public void ToArray_EmptyStack_StackException()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            //act
            //assert
            Assert.Throws<StackException>(() => stack.ToArray());
        }


        [Test]
        public void Clone_Invoke_StackReturned<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(item);
            stack.Push(item);
            Stack<T> stack2;
            //act
            stack2 = stack.Clone() as Stack<T>;
            //assert
            CollectionAssert.AreEqual(stack2, stack);
        }
        [Test]
        public void Clone_EmptyStack_StackException()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            //act
            //assert
            Assert.Throws<StackException>(() => stack.Clone());
        }
        [Test]
        public void Clone_CloneStack_StackClonedDelegateEventRaised<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            Stack<T> stack2;
            bool EventRaised = false;
            stack.StackCloned += (object st, Stack<T> clonedStack) =>
            {
                if (stack == st)
                {
                    CollectionAssert.AreEqual(stack, clonedStack, "Wrong cloned stack was sent by event");
                    EventRaised = true;
                }
            };
            stack.Push(item);
            //act
            stack2 = stack.Clone() as Stack<T>;
            //assert
            Assert.IsTrue(EventRaised);
        }

        [Test]
        public void CopyTo_Array_ArrayFilled<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(item);
            stack.Push(item);
            T[] arr = new T[stack.Count];
            //act
            stack.CopyTo(arr);
            //assert
            CollectionAssert.AreEqual(arr, stack);
        }
        [Test]
        public void CopyTo_EmptyStack_StackException()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            //act
            int[] arr = new int[stack.Count];
            //assert
            Assert.Throws<StackException>(() => stack.CopyTo(arr));
        }
        [Test]
        public void CopyTo_IncorrectIndex_StackException()
        {
            //arrange
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            //act
            int[] arr = new int[stack.Count];
            //assert
            Assert.Throws<StackException>(() => stack.CopyTo(arr, -1));
        }
        [Test]
        public void CopyTo_SmallerArray_StackException<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(item);
            stack.Push(item);
            //act
            T[] arr = new T[1];
            //assert
            Assert.Throws<IndexOutOfRangeException>(() => stack.CopyTo(arr));
        }


        [Test]
        public void Current_GetCurrennt_HeadItemReturned<T>([Values("aaa", 1)] T item)
        {
            //arrange
            Stack<T> stack = new Stack<T>();
            stack.Push(default);
            stack.Push(item);
            //act
            stack.Reset();
            stack.MoveNext();
            //assert
            Assert.AreEqual(stack.Current, item);
        }

    }
}