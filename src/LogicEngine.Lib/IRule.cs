using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IRule<T> where T : class
    {
        IEngineResult Execute(T model);
    }
}
