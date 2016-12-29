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

        public IList<IEngineResult> Execute(T model)
        {
	        this.RunElapsed = new TimeSpan(0,0,0,0,0);

			if (RunBumperRules)
            {
                this.results.Add(new PreRunRule<T>().Execute(model).End());
            }

            foreach (IRule<T> rule in rules)
            {
				this.results.Add(rule.Execute(model).End());
            }

            if (RunBumperRules)
            {
                this.results.Add(new PostRunRule<T>().Execute(model).End());
	            this.RunElapsed = this.results.First().TimeStart - this.results.Last().TimeEnd;
            }
	        formatter?.OutputResults(results,this.RunElapsed);
	        return this.results;
        }
    }
}
