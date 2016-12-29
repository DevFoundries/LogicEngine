using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicEngine.Lib.Test
{
	[TestClass]
	public class PreRunRuleTest
	{

		[TestMethod]
		public void ConstructorTest()
		{
			var rule = new PreRunRule<string>();
			var result = rule.Execute("noop");
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TimeStart);
			Assert.IsNotNull(result.TimeEnd);
		}
	}

	[TestClass]
	public class PostRunRuleTest
	{

		[TestMethod]
		public void ConstructorTest()
		{
			var rule = new PostRunRule<string>();
			var result = rule.Execute("noop");
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TimeStart);
			Assert.IsNotNull(result.TimeEnd);
		}
	}

}
