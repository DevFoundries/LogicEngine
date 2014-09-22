using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
    [TestClass]
    public class RuleBaseTest
    {
        [TestMethod]
        public void PreRunTest()
        {
            PreRunRule<string> ruleBase = new PreRunRule<string>();
            Assert.IsNotNull(ruleBase);
            var result = ruleBase.Execute("blah");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("PreRun"));

        }

        [TestMethod]
        public void PostTest()
        {
            PostRunRule<string> ruleBase = new PostRunRule<string>();
            ruleBase.Execute("blah");
            var result = ruleBase.Execute("blah");
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Message.Contains("PostRun"));
        }
    }
}
