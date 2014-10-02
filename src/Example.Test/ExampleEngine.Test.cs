using System;
using System.Collections;
using System.Collections.Generic;
using LogicEngine.Lib;
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

        [TestMethod]
        public void RunTest()
        {
            ExampleEngine engine = new ExampleEngine();
            var model = new ExampleModel() {Value1 = 1, Value2 = 2};
            IList<IEngineResult> results = engine.Run(model);
            Assert.AreEqual(3,model.AddResult);
        }
    }
}
