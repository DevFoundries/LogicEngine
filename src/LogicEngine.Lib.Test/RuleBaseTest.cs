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
            RuleBase ruleBase = new RuleBase();
            Assert.IsNotNull(ruleBase);
        }
    }
}
