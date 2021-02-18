using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace Extra
{
    [TestClass]
    public class IntegerToArrayTest
    {
        [TestMethod]
        public void array_to_integer()
        {
            Assert.AreEqual(
                123,
                ArrayToInteger(new int[] { 1, 2, 3 }));

            Assert.AreEqual(
                12,
                ArrayToInteger(new int[] { 0, 1, 2 }));
        }

        private int ArrayToInteger(int[] vs)
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void integer_to_array()
        {
            CollectionAssert.AreEqual(
                new[] { 1, 1, 2, 1, 1, 2, 3, 0 },
                IntegerToArray(11211230)
            );

            CollectionAssert.AreEqual(
                new[] { 5 },
                IntegerToArray(5)
            );

            CollectionAssert.AreEqual(
                new[] { 1, 3, 0, 0, 1, 1, 2, 0 },
                IntegerToArray(13001120)
            );
            CollectionAssert.AreEqual(
                new[] { 1, 0, 0, 0 },
                IntegerToArray(1000)
            );
        }

        private ICollection IntegerToArray(int v)
        {
            throw new NotImplementedException();
        }
    }
}
