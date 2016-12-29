using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib.Formatters
{
	// This formatter will do nothing. It's super useful :)
	public class NoopFormatter : IResultsFormatter
	{
		public void OutputResults(IList<IEngineResult> results, TimeSpan totalTime)
		{
			// noop
		}
	}
}
