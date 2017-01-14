namespace LogicEngine.Lib.Test.TestObjects
{
	public class Subtract : IRule<TestModel>, IOtherInterface
	{
		public IEngineResult Execute(TestModel model)
		{
			EngineResult result = new EngineResult() { Name = GetType().ToString() };
			model.Result = model.A*4 - model.B;
			return result;
		}
	}
}