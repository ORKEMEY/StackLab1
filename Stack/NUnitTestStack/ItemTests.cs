using Stack;
using NUnit.Framework;

namespace NUnitTestStack
{
	[TestFixture]
	class ItemTests
	{
		[Test]
		public void Index_GetIndexOfSecondItem_1Returned()
		{
			//arrange
			Item<int> firstitem = new Item<int>(null);
			Item<int> secoditem = new Item<int>(firstitem);
			int expectedIndex;
			//act
			expectedIndex = 1;
			//assert
			Assert.AreEqual(expectedIndex, secoditem.Index);
		}


		[Test]
		public void Constructor_InitializeChain_ChainRefsAreCorrect()
		{
			//arrange
			Item<int> firstitem = new Item<int>(null);
			Item<int> secoditem = new Item<int>(firstitem);
			//act
			//assert
			Assert.AreEqual(firstitem, secoditem.Prev);

		}
		[Test]
		public void Constructor_InitializeItemsWithObj_ItemsKeepsObj()
		{
			//arrange
			Item<int> firstitem = new Item<int>(null, 1);
			Item<int> secoditem = new Item<int>(firstitem, 2);
			int expectedInt = 2;
			//act
			//assert
			Assert.AreEqual(expectedInt, secoditem.Object);
		}

	}
}
