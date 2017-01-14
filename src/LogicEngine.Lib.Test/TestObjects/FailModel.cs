namespace LogicEngine.Lib.Test
{
	public class FailModel : IRule<BadModel>, IOtherInterface
	{
		public IEngineResult Execute(BadModel model)
		{
			EngineResult result = new EngineResult() { Name = GetType().ToString() };
			return result;
		}
	}
}