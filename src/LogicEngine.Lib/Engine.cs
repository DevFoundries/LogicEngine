using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IEngine<T> where T : class
    {
        IList<IEngineResult> Execute(T model);
        bool RunBumperRules { get; set; }
		TimeSpan LastRunElapsed { get; }
    }

    public class Engine<T> : IEngine<T> where T : class
    {
        private IRuleCollection<T> rules;
        IList<IEngineResult> results = new List<IEngineResult>();

        public Engine(IRuleCollection<T> rules)
        {
            this.rules = rules;
        }

        public IList<IEngineResult> Execute(T model)
        {
	        this.LastRunElapsed = new TimeSpan(0,0,0,0,0);

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
	            this.LastRunElapsed = this.results.First().TimeStart - this.results.Last().TimeEnd;
            }
			return this.results;
        }

        public bool RunBumperRules { get; set; }
	    public TimeSpan LastRunElapsed { get; private set; }
    }

}
