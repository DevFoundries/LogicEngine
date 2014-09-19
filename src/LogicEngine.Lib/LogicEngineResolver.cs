using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace LogicEngine.Lib
{
    public class LogicEngineResolver
    {
        private static LogicEngineResolver INSTANCE = new LogicEngineResolver();
        private IUnityContainer container;

        private LogicEngineResolver()
        {
            Init();
        }

        private void Init()
        {
            container = new UnityContainer();
            Register(container);
        }

        private void Register(IUnityContainer container)
        {
            
        }

        public static LogicEngineResolver Instance { get { return INSTANCE; } }

        public IUnityContainer Container { get { return container; } }

        public void Reset()
        {
            Init();
        }
    }
}
