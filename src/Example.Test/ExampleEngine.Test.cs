using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			Assert.AreEqual(2, model.MultiplicaionResult);
			Assert.AreEqual(0.5, model.DivisionResult);
			Assert.AreEqual(-1,model.SubtractResult);
			Assert.IsTrue(results.First().Message.Contains("PreRun"));
			Assert.IsTrue(results.Last().Message.Contains("PostRun"));
	        TimeSpan elapsed = results.First().TimeStart - results.Last().TimeEnd; // the bumper rules have the start/stop times.
			Assert.IsNotNull(elapsed);
        }
	}
}
