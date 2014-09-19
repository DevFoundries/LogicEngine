using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
    [TestClass]
    public class RuleBaseTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            RuleBase<string> ruleBase = new RuleBase<string>();
            Assert.IsNotNull(ruleBase);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            RuleBase<string> ruleBase = new RuleBase<string>();
            ruleBase.Execute("blah");

        }
    }
}
