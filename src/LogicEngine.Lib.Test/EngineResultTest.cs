using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
    [TestClass]
    public class EngineResultTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            IEngineResult result = new EngineResult() { Error = "error", Message = "Message" }.End();
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.IsNotNull(result.Message);
            Assert.IsNotNull(result.TimeEnd);
            Assert.IsNotNull(result.TimeStart);
			Assert.IsNotNull(result.Elapsed);
            Assert.IsTrue(result.HasError);
        }
    }
}
