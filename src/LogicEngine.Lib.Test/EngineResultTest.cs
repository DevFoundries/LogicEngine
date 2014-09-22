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
            EngineResult result = new EngineResult() { Error = "error", Message = "Message", TimeEnd = DateTime.UtcNow, TimeStart = DateTime.UtcNow};
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.IsNotNull(result.Message);
            Assert.IsNotNull(result.TimeEnd);
            Assert.IsNotNull(result.TimeStart);
            Assert.IsTrue(result.HasError);
        }
    }
}
