using System;
using System.Collections.Generic;
using LogicEngine.Lib.Formatters;
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
			Assert.IsNotNull(engine.RunElapsed);
			Assert.IsTrue(engine.RunElapsed < TimeSpan.FromMilliseconds(100));
        }

	    [TestMethod]
	    public void RunWithFormaterTest()
	    {
		    var formatter = new CsvResultsFormatter();
			Engine<string> engine = new Engine<string>(new RuleCollection<string>(), formatter) { RunBumperRules = true };
			var result = engine.Execute("blah");
			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count);
			Assert.IsNotNull(engine.RunElapsed);
			Assert.IsNotNull(formatter.Output);
			Assert.IsTrue(engine.RunElapsed < TimeSpan.FromMilliseconds(100));

		}

		[TestMethod]
        public void ExecuteViaUnity()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IEngine<string>,Engine<string>>();
	        container.RegisterType<IResultsFormatter, NoopFormatter>();
            IRuleCollection<string> coll = new RuleCollection<string>();
            container.RegisterInstance(coll);
            var engine = container.Resolve<IEngine<string>>();
            Assert.IsNotNull(engine);
        }
    }
}
