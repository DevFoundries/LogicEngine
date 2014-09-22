using System;
using System.Collections.Generic;
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
            Engine<string> engine = new Engine<string>(new List<IRule<string>>());
            var result = engine.Execute("blah");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RunBumpersTest()
        {
            Engine<string> engine = new Engine<string>(new List<IRule<string>>()) { RunBumperRules = true };
            var result = engine.Execute("blah");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }


    }
}
