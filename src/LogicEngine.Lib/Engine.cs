using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IEngine<T> where T : class
    {
        void Execute(T model);
    }

    public class Engine<T> : IEngine<T> where T : class
    {
        private IList<IRule<T>> rules;

        public Engine(IList<IRule<T>> rules )
        {
            this.rules = rules;
        }

        public void Execute(T model)
        {
            foreach (IRule<T> rule in rules)
            {
                rule.Execute(model);
            }
        }
    }

}
