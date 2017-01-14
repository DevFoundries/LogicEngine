using System;
using System.Collections.Generic;
using LogicEngine.Lib.Formatters;
using LogicEngine.Lib.Test.TestObjects;
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

	    [TestMethod]
	    public void ValidateContraintTest()
	    {
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
		    var engine = container.Resolve<IEngine<TestModel>>();
			engine.ValidateContraints(new List<Type>()
			{
				typeof(Subtract)
			});
	    }

		[TestMethod]
		[ExpectedException(typeof(ArgumentException),AllowDerivedTypes = false)]
		public void ValidateContraintFailTest()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
			var engine = container.Resolve<IEngine<TestModel>>();
			engine.ValidateContraints(new List<Type>()
			{
				typeof(Fail)
			});
		}

		[TestMethod]
		public void ValidateContraintNoopTest()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
			var engine = container.Resolve<IEngine<TestModel>>();
			engine.ValidateContraints(null);
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentException), AllowDerivedTypes = false)]
		public void ValidateContraintFailModelTypeTest()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
			var engine = container.Resolve<IEngine<TestModel>>();
			engine.ValidateContraints(new List<Type>()
			{
				typeof(FailModel)
			});
		}

		[TestMethod]
		public void ExcludeRuleTest()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
			var engine = container.Resolve<IEngine<TestModel>>();
			var model = new TestModel() {A = 2, B = 4};
			var results = engine.Execute(model, new List<Type>() {typeof(Subtract)}, null);
			Assert.AreEqual(1, results.Count,0,"Rule count not correct."); // only one rule should run
			Assert.AreEqual(6,model.Result);

		}

		[TestMethod]
		public void OnlyRuleTest()
		{
			UnityContainer container = new UnityContainer();
			container.RegisterType<IEngine<TestModel>, Engine<TestModel>>();
			IRuleCollection<TestModel> coll = new RuleCollection<TestModel>();
			container.RegisterType<IResultsFormatter, NoopFormatter>();
			coll.Add(new Add());
			coll.Add(new Subtract());
			container.RegisterInstance(coll);
			var engine = container.Resolve<IEngine<TestModel>>();
			var model = new TestModel() { A = 2, B = 4 };
			var results = engine.Execute(model, null, new List<Type>() { typeof(Subtract) });
			Assert.AreEqual(1, results.Count); // only one rule should run
			Assert.AreEqual(4, model.Result);
		}
	}
}
