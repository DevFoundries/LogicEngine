using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicEngine.Lib
{
    public interface IRuleCollection<T> : IList<IRule<T>> where T : class
    {
    }

    public class RuleCollection<T> : List<IRule<T>>, IRuleCollection<T> where T : class
    {
    }
}
