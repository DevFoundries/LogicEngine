using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IRule<T> where T : class
    {
        void Execute(T model);
    }

    
    public class RuleBase<T> : IRule<T> where T : class
    {

        public virtual void Execute(T model)
        {
        }

    }

}
