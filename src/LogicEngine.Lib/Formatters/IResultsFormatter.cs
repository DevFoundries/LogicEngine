using System;
using System.Collections.Generic;

namespace LogicEngine.Lib.Formatters
{
	public interface IResultsFormatter
	{
		void OutputResults(IList<IEngineResult> results, TimeSpan totalTime);
	}
}
