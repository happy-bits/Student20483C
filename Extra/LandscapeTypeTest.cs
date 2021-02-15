using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Extra
{
    [TestClass]
    public class LandscapeTypeTest
    {
        [TestMethod]
        [DataRow(new[] { 3, 4, 5, 4, 3 })]
        [DataRow(new[] { 1, 3, 5, 4, 3, 2 })]
        [DataRow(new[] { -1, 0, -1 })]
        [DataRow(new[] { -1, -1, 0, -1, -1 })]

        public void mountains(int[] arr)
        {
            Assert.AreEqual("mountain", LandscapeType(arr));
        }

        [TestMethod]
        [DataRow(new[] { 10, 9, 8, 7, 2, 3, 4, 5 })]
        [DataRow(new[] { 350, 100, 200, 400, 700 })]
        [DataRow(new[] { 9, 7, 3, 1, 2, 4 })]
        [DataRow(new[] { 9, 8, 9 })]
        [DataRow(new[] { 3, 2, 2, 1, 2 })]
        public void valleys(int[] arr)
        {
            Assert.AreEqual("valley", LandscapeType(arr));
        }

        [TestMethod]
        [DataRow(new[] { 9, 0, 0, 9, 0, 0 })] // två toppar
        [DataRow(new[] { 0, -1, -1, 0, -1, -1 })] // två toppar
        [DataRow(new[] { 1, 2, 3, 2, 4, 1 })] // två toppar
        [DataRow(new[] { 5, 4, 3, 2, 1 })]    // kant
        [DataRow(new[] { 9, 8, 9, 8 })] 
        public void neithers(int[] arr)
        {
            Assert.AreEqual("neither", LandscapeType(arr));
        }

        private string LandscapeType(int[] arr)
        {
            throw new NotImplementedException();
        }
    }
}
