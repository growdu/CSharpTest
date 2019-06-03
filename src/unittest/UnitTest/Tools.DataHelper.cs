using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tools.DataHelper;

namespace UnitTest
{
    [TestClass]
    public class HelperTest
    {
        [TestMethod]
        public void IsEmpty()
        {
            Assert.AreEqual("".IsEmpty(),true);
            Assert.AreEqual("   ".IsEmpty(),true);
            Assert.AreEqual("sdds".IsEmpty(),false);
        }
    }
}
