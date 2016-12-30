using System;
using System.Collections.Generic;
using System.Text;

namespace LogicEngine.Lib.Formatters
{
	public interface ICsvResultsFormatter : IResultsFormatter
	{
		string Output { get; }
	}

	public class CsvResultsFormatter : ICsvResultsFormatter
	{
		public string Output { get; private set; }


		public void OutputResults(IList<IEngineResult> results, TimeSpan runElapsed)
		{
			var format = "{0},{1},{2},{3},{4},{5},{6}\r\n";
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("Run Elapsed Total Time: " + runElapsed);
			sb.AppendFormat(format, "RuleName", "Start", "Stop", "Elapsed", "HasError","Message" ,"ErrorMessage");
			foreach (var result in results)
			{
				sb.AppendFormat(format, result.Name, result.TimeStart,result.TimeEnd, result.Elapsed, result.HasError, result.Message,result.Error);
			}
			this.Output = sb.ToString();
		}
	}
}
