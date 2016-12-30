[![Get help on Codementor](https://cdn.codementor.io/badges/get_help_github.svg)](https://www.codementor.io/wbsimms?utm_source=github&utm_medium=button&utm_term=wbsimms&utm_campaign=github)

[![Feature Requests](http://feathub.com/wbsimms/LogicEngine?format=svg)](http://feathub.com/wbsimms/LogicEngine)


LogicEngine
===========

LogicEngine is designed to run arbitrary rules or bits of logic against a given model. It's written in C# .NET 4.6.2.

This project is born out of a DRY (don't repeat yourself) mentality. In other words, I was using the same code on several projects.

How to use
-----------

The source code has a working example (ExampleEngine) which shows how easy the logic engine is to use. The steps to use LogicEngine are as follows:
1. Create your business model
```C#
public class ExampleModel
{
    public int Value1 { get; set; }
    public int Value2 { get; set; }
    public int AddResult { get; set; }
    public int SubtractResult { get; set; }
    public int MultiplicaionResult { get; set; }
    public float DivisionResult { get; set; }
}
```
2. Create rules which implement IRule\<T>
```c#
public class AddRule : IRule<ExampleModel> 
{
    public IEngineResult Execute(ExampleModel model)
    {
        EngineResult result = new EngineResult() { Name = GetType().ToString() };
        model.AddResult = model.Value1 + model.Value2;
        return result;
    }
}
```
3. Add the rules to the rule collection
4. Create the engine with the collection
5. Execute the engine with your business model

```c#
Engine<ExampleModel> engine = new Engine<ExampleModel>(
    new RuleCollection<ExampleModel>()
    {
        new AddRule(), 
        new DivisionRule(), 
        new MultiplicationRule(), 
        new SubtractRule()
    }) {RunBumperRules = true};
var retval = engine.Execute(model);
```

The engine has the following interface:
```c#
public interface IEngine<T> 
	where T: class 
{
    IList<IEngineResult> Execute(T model);
    bool RunBumperRules { get; set; }
    TimeSpan RunElapsed { get; }
}
```
RunBumperRulles will insert PreRun and PostRun rules as the first and last rules. These are used to determine RunElapsed timespan.

The engine returns a list of IEngineResult:
```c#
public interface IEngineResult
{
    string Name { get; set; }
    bool HasError { get; }
    string Error { get; set; }
    string Message { get; set; }
    DateTime TimeStart { get; }
    DateTime TimeEnd { get; }
    TimeSpan Elapsed { get; }
    IEngineResult End(); // You should never need to call this.
}
```
Each IEngineResult will return the Elapsed TimeSpan. It's your reponsiblity to set Name, Error, and Message.

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
        EngineResult result = new EngineResult() { Name = GetType().ToString() };
        model.AddResult = model.Value1 + model.Value2;
        return result;
    }
}
```
The EngineResult class can be implemented, or you can use your own. The Engine returns a list of EngineResults. You can add any information you'd like.

Once the rules are created, you only need to create an engine with them and execute the rules against the model.

```c#
public IList<IEngineResult> Run(ExampleModel model)
{
    Engine<ExampleModel> engine = new Engine<ExampleModel>(
        new RuleCollection<ExampleModel>()
        {
            new AddRule(), 
            new DivisionRule(), 
            new MultiplicationRule(), 
            new SubtractRule()
        }) {RunBumperRules = true};
    var retval = engine.Execute(model);
    return retval;
}
```

Bumper Rules
------------
The engine has some "bumper rules". They're rules that run before/after all your rules. They will give you run start/stop times.

```c#
new Engine<SomeModel>(someListOfRules) {RunBumperRules = true;}
```

Results Formatter
------------
The engine has support for formatting the list of IEngineResults. There are two provided formatters:
1. NoopFormatter
This formatter does nothing. It's the default formatter if you don't provide one.
2. CsvFormatter
This formatter returns a CSV list of the results. NOTE: The first row will the total elapsed run time.

```c#
public Engine(IRuleCollection<T> rules, IResultsFormatter resultFormatter = null)
```
The only requirement to implement your own formatter is to implement the IResultsFormatter interface as seen in the CsvResultsFormatter:
```c#
public interface IResultsFormatter
{
	void OutputResults(IList<IEngineResult> results, TimeSpan totalTime);
}

public class CsvResultsFormatter : ICsvResultsFormatter
{
	public string Output { get; private set; }
	public void OutputResults(IList<IEngineResult> results, TimeSpan runElapsed)
	{
		var format = "{0},{1},{2},{3},{4},{5},{6}\r\n";
		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Run Elapsed Total Time: " + runElapsed);
		sb.AppendFormat(format, "RuleName", "Start", "Stop", "Elapsed", "HasError","Message" ,"ErrorMessage");
		foreach (var result in results)
		{
			sb.AppendFormat(format, result.Name, result.TimeStart,result.TimeEnd, result.Elapsed, result.HasError, result.Message,result.Error);
		}
		this.Output = sb.ToString();
	}
}
```

Dependency Injection Support
-----------
You can use dependency injection to add your rules. Simply add them to the RulesCollection.

```c#
UnityContainer container = new UnityContainer();
container.RegisterType<IEngine<string>,Engine<string>>();
container.RegisterType<IResultsFormatter, NoopFormatter>();
IRuleCollection<string> coll = new RuleCollection<string>();
container.RegisterInstance(coll);
var engine = container.Resolve<IEngine<string>>();
Assert.IsNotNull(engine);
```

