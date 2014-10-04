using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicEngine.Lib;

namespace Example
{
    public class ExampleEngine
    {
        public IList<IEngineResult> Run(ExampleModel model)
        {
            Engine<ExampleModel> engine = new Engine<ExampleModel>(
                new RuleCollection<ExampleModel>()
                {
                    new AddRule(), 
                    new DivisionRule(), 
                    new MultiplicationRule(), 
                    new SubtractRule()
                });
            var retval = engine.Execute(model);
            return retval;
        }
    }
}
