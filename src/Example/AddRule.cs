using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicEngine.Lib;

namespace Example
{
    public class AddRule : IRule<ExampleModel> 
    {
        public IEngineResult Execute(ExampleModel model)
        {
            EngineResult result = new EngineResult() { TimeStart = DateTime.UtcNow, Name = GetType().ToString() };
            model.AddResult = model.Value1 + model.Value2;
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

    class SubtractRule : IRule<ExampleModel>
    {
        public IEngineResult Execute(ExampleModel model)
        {
            EngineResult result = new EngineResult() { TimeStart = DateTime.UtcNow, Name = GetType().ToString() };
            model.SubtractResult = model.Value1 - model.Value2;
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

    class MultiplicationRule : IRule<ExampleModel>
    {
        public IEngineResult Execute(ExampleModel model)
        {
            EngineResult result = new EngineResult() { TimeStart = DateTime.UtcNow, Name = GetType().ToString() };
            model.MultiplicaionResult = model.Value1 * model.Value2;
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

    class DivisionRule : IRule<ExampleModel>
    {
        public IEngineResult Execute(ExampleModel model)
        {
            EngineResult result = new EngineResult() { TimeStart = DateTime.UtcNow, Name = GetType().ToString()};
            model.DivisionResult = (float)model.Value1 / model.Value2;
            result.TimeEnd = DateTime.UtcNow;
            return result;
        }
    }

}
