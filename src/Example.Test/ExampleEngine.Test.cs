using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Example.Test
{
    [TestClass]
    public class ExampleEngineTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ExampleEngine engine = new ExampleEngine();
            Assert.IsNotNull(engine);
        }
    }
}
