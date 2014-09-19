using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Engine engine = new Engine();
            Assert.IsNotNull(engine);
        }
    }
}
