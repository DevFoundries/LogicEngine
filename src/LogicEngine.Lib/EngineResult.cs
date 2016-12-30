using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IEngineResult
    {
        string Name { get; set; }
        bool HasError { get; }
        string Error { get; set; }
        string Message { get; set; }
        DateTime TimeStart { get; }
        DateTime TimeEnd { get; }
	    TimeSpan Elapsed { get; }
		IEngineResult End();
    }

    public class EngineResult : IEngineResult
    {
        public EngineResult()
        {
        }

        public bool HasError => !string.IsNullOrEmpty(Error);

	    public string Name { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
	    public DateTime TimeStart { get; } = DateTime.UtcNow;
		public DateTime TimeEnd { get; private set; }
	    public TimeSpan Elapsed { get; private set; }

	    public IEngineResult End()
	    {
		    this.TimeEnd = DateTime.UtcNow;
		    this.Elapsed = this.TimeStart - this.TimeEnd;
		    return this;
	    }
    }
}
