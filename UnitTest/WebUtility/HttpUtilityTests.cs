using Microsoft.VisualStudio.TestTools.UnitTesting;
using vevisoft.WebUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vevisoft.WebUtility.Tests
{
    [TestClass()]
    public class HttpUtilityTests
    {
        [TestMethod()]
        public void GetHtmlStringFromUrlByGetTest()
        {
            HttpParam hp = new HttpParam("http://www.baidu.com");
            string str = HttpUtility.GetHtmlStringFromUrlByGet(hp);            
            Assert.IsTrue(str.Contains("百度"));
            //Assert.Fail();
        }
    }
}