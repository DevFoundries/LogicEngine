using System;
using System.Collections.Generic;
using LogicEngine.Lib.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
	[TestClass]
	public class PrintResultsFormatterTest
	{
		[TestMethod]
		public void ConstructorTest()
		{
			var formatter = new CsvResultsFormatter();
			Assert.IsNotNull(formatter);
		}

		[TestMethod]
		public void OutputTest()
		{
			IList<IEngineResult> list = new List<IEngineResult>();
			list.Add(new EngineResult() { Name = "Name 1" }.End());
			list.Add(new EngineResult() { Name = "Name 2" }.End());
			list.Add(new EngineResult() { Name = "Name 3" }.End());
			list.Add(new EngineResult() { Name = "Name 4" }.End());
			list.Add(new EngineResult() { Name = "Name 5" }.End());
			var formatter = new CsvResultsFormatter();
			formatter.OutputResults(list,new TimeSpan(0,0,0,4,100));
			Assert.IsNotNull(formatter.Output);
			Assert.IsInstanceOfType(formatter.Output, typeof(string));

		}
	}
}
