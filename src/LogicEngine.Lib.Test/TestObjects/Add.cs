namespace LogicEngine.Lib.Test.TestObjects
{
	public class Add : IRule<TestModel>, IOtherInterface
	{
		public IEngineResult Execute(TestModel model)
		{
			EngineResult result = new EngineResult() { Name = GetType().ToString() };
			model.Result = model.A + model.B;
			return result;
		}
	}
}