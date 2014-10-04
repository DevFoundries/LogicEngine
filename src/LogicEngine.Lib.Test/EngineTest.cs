using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
    [TestClass]
    public class EngineTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Engine<string> engine = new Engine<string>(null);
            Assert.IsNotNull(engine);
        }

        [TestMethod]
        public void ExecuteTest()
        {
            Engine<string> engine = new Engine<string>(new RuleCollection<string>());
            var result = engine.Execute("blah");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RunBumpersTest()
        {
            Engine<string> engine = new Engine<string>(new RuleCollection<string>()) { RunBumperRules = true };
            var result = engine.Execute("blah");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void ExecuteViaUnity()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IEngine<string>,Engine<string>>();
            IRuleCollection<string> coll = new RuleCollection<string>();
            container.RegisterInstance(coll);

            var engine = container.Resolve<IEngine<string>>();
            Assert.IsNotNull(engine);
        }
    }
}
