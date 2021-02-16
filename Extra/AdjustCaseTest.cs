
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Extra
{
    [TestClass]
    public class AdjustCaseTest
    {
        [TestMethod]
        [DataRow("rhino", "cAt", "rHino")]
        [DataRow("cat", "RHino", "CAt")]
        [DataRow("rhino", "aAaaA", "rHinO")]
        [DataRow("RHINO", "aAaaA", "rHinO")]
        [DataRow("RHINO", "aAaaAaAaAaA", "rHinO")]   // längre template
        [DataRow("RHINOoOoO", "aAaaA", "rHinOoOoO")] // längre ord
        public void adjust_case(string word, string template, string expected)
        {
            Assert.AreEqual(expected, AdjustCase(word, template));
        }

        private string AdjustCase(string word, string template)
        {
            throw new NotImplementedException();
        }
    }
}
