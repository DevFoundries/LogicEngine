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
            IEngineResult result = new EngineResult() {TimeStart = DateTime.UtcNow};
            result.Message = this.GetType() + " :: Start ";
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

    public class PostRunRule<T> : IRule<T> where T : class
    {
        public IEngineResult Execute(T model)
        {
            IEngineResult result = new EngineResult() { TimeStart = DateTime.UtcNow };
            result.Message = this.GetType() + " :: End ";
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

}
