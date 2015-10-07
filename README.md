[![Get help on Codementor](https://cdn.codementor.io/badges/get_help_github.svg)](https://www.codementor.io/wbsimms?utm_source=github&utm_medium=button&utm_term=wbsimms&utm_campaign=github)


LogicEngine
===========

[![Feature Requests](http://feathub.com/wbsimms/LogicEngine?format=svg)](http://feathub.com/wbsimms/LogicEngine)

LogicEngine is designed to run arbitrary rules or bits of logic against a given model. It's written in C# .NET 4.5.1.

This project is born out of a DRY (don't repeat yourself) mentality. In other words, I was using the same code on several projects.

 How to use
-----------

The source code has a working example (ExampleEngine) which shows how easy the logic engine is to use.

API
-----------

The only real requirement is to implement the IRule interface:

```c#
public interface IRule<T> where T : class
{
    IEngineResult Execute(T model);
}
```

In the ExampleEngine, the AddRule class looks like this:

```c#
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
```

The EngineResult class can be implemented, or you can use your own. The Engine returns a list of EngineResults. You can add any information you'd like.

Once the rules are created, you only need to create an engine with them and execute the rules against the model.

```c#
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
```

Bumper Rules
------------
The engine has some "bumper rules". They're rules that run before/after all your rules. They will give you run start/stop times.

```c#
new Engine<SomeModel>(someListOfRules) {RunBumperRules = true;}
```

Dependency Injection Suppot
-----------
You can use dependency injection to add your rules. Simply add them to the RulesCollection.

```c#
UnityContainer container = new UnityContainer();
container.RegisterType<IEngine<string>,Engine<string>>();
IRuleCollection<string> coll = new RuleCollection<string>() {rule1, rule2, rule3};
container.RegisterInstance(coll);
var engine = container.Resolve<IEngine<string>>();
```

