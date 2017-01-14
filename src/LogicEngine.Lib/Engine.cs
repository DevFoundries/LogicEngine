using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicEngine.Lib.Formatters;

namespace LogicEngine.Lib
{
	public interface IEngine<T> 
		where T: class 
    {
        IList<IEngineResult> Execute(T model);
	    IList<IEngineResult> Execute(T model, IEnumerable<Type> excludeRules, IEnumerable<Type> onlyRules);
	    void ValidateContraints(IEnumerable<Type> excludeRules);

		bool RunBumperRules { get; set; }
		TimeSpan RunElapsed { get; }
    }

    public class Engine<T> : IEngine<T> 
		where T : class
	{
		private IRuleCollection<T> rules;
		private IResultsFormatter formatter;
		private IList<IEngineResult> results = new List<IEngineResult>();

		public bool RunBumperRules { get; set; }
		public TimeSpan RunElapsed { get; private set; }

		public Engine(IRuleCollection<T> rules, IResultsFormatter resultFormatter = null)
        {
            this.rules = rules;
	        this.formatter = resultFormatter;
			if (formatter == null)
				formatter = new NoopFormatter();
        }

		public IList<IEngineResult> Execute(T model, IEnumerable<Type> excludeRules, IEnumerable<Type> onlyRules)
		{
			ValidateContraints(excludeRules);
			ValidateContraints(onlyRules);
			this.RunElapsed = new TimeSpan(0, 0, 0, 0, 0);

			if (RunBumperRules)
			{
				this.results.Add(new PreRunRule<T>().Execute(model).End());
			}


			foreach (IRule<T> rule in rules)
			{
				if (excludeRules != null || onlyRules != null)
				{
					var shouldRunModel = new ShouldRunRuleModel()
					{
						ExcludeRules = excludeRules,
						OnlyRules = onlyRules,
						ShouldRunRule = true,
						RuleInQuestion = rule.GetType()
					};
					this.ShouldRunEngine.Execute(shouldRunModel);
					if (!shouldRunModel.ShouldRunRule) continue;
				}
				this.results.Add(rule.Execute(model)?.End());
			}

			if (RunBumperRules)
			{
				this.results.Add(new PostRunRule<T>().Execute(model).End());
				this.RunElapsed = this.results.First().TimeStart - this.results.Last().TimeEnd;
			}
			formatter?.OutputResults(results, this.RunElapsed);
			return this.results;
		}

		public void ValidateContraints(IEnumerable<Type> rulesToValidate)
		{
			var valRules = rulesToValidate?.ToList();
			if (valRules == null || !valRules.Any()) return;
			var checkType = typeof(IRule<T>);
			var i = valRules.SelectMany(x => x.GetInterfaces());
			if (!i.Contains(checkType))
			{
				throw new ArgumentException("Exclude and Only rule parameters must all implement the IRule<T> interface");
			}
		}


		public IList<IEngineResult> Execute(T model)
		{
			return this.Execute(model, null, null);
		}

		public IEngine<ShouldRunRuleModel> ShouldRunEngine
		{
			get
			{
				return new Engine<ShouldRunRuleModel>(new RuleCollection<ShouldRunRuleModel>()
				{
					new IsExcludedRule(),
					new RunOnlyRule()
				});
			}
		}
	}

	public class ShouldRunRuleModel
	{
		public bool ShouldRunRule { get; set; }
		public Type RuleInQuestion { get; set; }
		public IEnumerable<Type> ExcludeRules { get; set; }
		public IEnumerable<Type> OnlyRules { get; set; }
	}

	public class IsExcludedRule : IRule<ShouldRunRuleModel> {
		public IEngineResult Execute(ShouldRunRuleModel model)
		{
			if (model.ExcludeRules == null || model.OnlyRules != null)
			{
				return null;
			}
			if (model.ExcludeRules.Contains(model.RuleInQuestion))
			{
				model.ShouldRunRule = false;
			}
			return null;
		}
	}

	public class RunOnlyRule : IRule<ShouldRunRuleModel>
	{
		public IEngineResult Execute(ShouldRunRuleModel model)
		{
			if (model.OnlyRules == null)
			{
				return null;
			}

			model.ShouldRunRule = model.OnlyRules.Contains(model.RuleInQuestion);
			return null;
		}
	}

}
