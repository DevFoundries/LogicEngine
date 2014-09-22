using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IEngineResult
    {
        bool HasError { get; }
        string Error { get; set; }
        string Message { get; set; }
        DateTime TimeStart { get; set; }
        DateTime TimeEnd { get; set; }
    }

    public class EngineResult : IEngineResult
    {
        public EngineResult()
        {
        }


        public bool HasError {
            get { return !string.IsNullOrEmpty(Error); }
        }
        public string Error { get; set; }
        public string Message { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
    }
}
