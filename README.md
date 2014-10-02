LogicEngine
===========

LogieEngine is designed to run arbitrary rules or bits of logic against a given model. It's written in C# .NET 4.5.1.

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
    new List<IRule<ExampleModel>>()
    {
        new AddRule(), 
        new DivisionRule(), 
        new MultiplicationRule(), 
        new SubtractRule()
    });
var retval = engine.Execute(model);
return retval;
```

Note : You should use dependency injection here.

Bumper Rules
------------
The engine has some "bumper rules". They're rules that run before/after all your rules. They will give you run start/stop times.

```c#
new Engine<SomeModel>(someListOfRules) {RunBumperRules = true;}
```


