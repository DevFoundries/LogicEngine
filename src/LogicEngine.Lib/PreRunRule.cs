using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public class PreRunRule<T> : IRule<T> where T: class
    {
        public IEngineResult Execute(T model)
        {
            IEngineResult result = new EngineResult();
            result.Message = this.GetType() + " :: Start ";
            return result;
        }
    }

    public class PostRunRule<T> : IRule<T> where T : class
    {
        public IEngineResult Execute(T model)
        {
            IEngineResult result = new EngineResult();
            result.Message = this.GetType() + " :: End ";
            return result;
        }
    }

}
