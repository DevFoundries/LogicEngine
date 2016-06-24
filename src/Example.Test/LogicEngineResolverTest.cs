using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Example.Test
{
    [TestClass]
    public class LogicEngineResolverTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var resolver = LogicEngineResolver.Instance;
            Assert.IsNotNull(resolver);
            Assert.IsNotNull(resolver.Container);
            resolver.Reset();
        }
    }
}
