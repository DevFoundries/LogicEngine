using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IEngine<T> where T : class
    {
        IList<IEngineResult> Execute(T model);
        bool RunBumperRules { get; set; }
    }

    public class Engine<T> : IEngine<T> where T : class
    {
        private IList<IRule<T>> rules;
        IList<IEngineResult> results = new List<IEngineResult>();

        public Engine(IList<IRule<T>> rules )
        {
            this.rules = rules;
        }

        public IList<IEngineResult> Execute(T model)
        {
            if (RunBumperRules)
            {
                this.results.Add(new PreRunRule<T>().Execute(model));
            }

            foreach (IRule<T> rule in rules)
            {
                this.results.Add(rule.Execute(model));
            }

            if (RunBumperRules)
            {
                this.results.Add(new PostRunRule<T>().Execute(model));
            }
            return this.results;
        }

        public bool RunBumperRules { get; set; }
    }

}
