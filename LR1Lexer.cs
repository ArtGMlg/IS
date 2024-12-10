using System.Text.Json;
using Helpers;

public class LR1Lexer
{
  private readonly Dictionary<int, Dictionary<string, (string, int)>> actions;
  private readonly Dictionary<int, Dictionary<string, int>> transitions;
  private readonly Dictionary<string, IStateFactory> actionToState;
  private readonly List<(string, List<string>)> grammar;

  public LR1Lexer(
    string actionsPath,
    string transitionsPath,
    string grammarPath
  )
  {
    this.actions = LoadUpActions(actionsPath);
    this.transitions = LoadUpTransitions(transitionsPath);
    this.grammar = LoadUpGrammar(grammarPath);

    actionToState = new Dictionary<string, IStateFactory>
    {
      { "s", new ShiftStateFactory() },
      { "r", new ReduceStateFactory() },
      { "a", new FinalStateFactory() }
    };
  }

  private Dictionary<int, Dictionary<string, (string, int)>> LoadUpActions(string actionsPath)
  {
    try
    {
      using StreamReader reader = new(actionsPath);

      string json = reader.ReadToEnd();

      var jsonOptions = new JsonSerializerOptions()
      {
        IncludeFields = true
      };

      var decodedJson = JsonSerializer.Deserialize<Dictionary<int, Dictionary<string, (string, int)>>>(json, jsonOptions);

      SkipOrThrow.NotNullable(decodedJson);

      return decodedJson;

    }
    catch (IOException e)
    {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
      return [];
    }
    catch (Exception e)
    {
      Console.WriteLine("Table wasn't setup correctly:");
      Console.WriteLine(e.Message);
      return [];
    }
  }

  private Dictionary<int, Dictionary<string, int>> LoadUpTransitions(string transitionsPath)
  {
    try
    {
      using StreamReader reader = new(transitionsPath);

      string json = reader.ReadToEnd();

      var decodedJson = JsonSerializer.Deserialize<Dictionary<int, Dictionary<string, int>>>(json);

      SkipOrThrow.NotNullable(decodedJson);

      return decodedJson;

    }
    catch (IOException e)
    {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
      return [];
    }
    catch (Exception e)
    {
      Console.WriteLine("Table wasn't setup correctly:");
      Console.WriteLine(e.Message);
      return [];
    }
  }

  private List<(string, List<string>)> LoadUpGrammar(string grammarPath)
  {
    try
    {
      using StreamReader reader = new(grammarPath);

      string file = reader.ReadToEnd();

      List<(string, List<string>)> values = [];

      file.Split('\n').Select(s => s.Trim()).ToList().ForEach(l =>
      {
        var ls = l.Split("->").Select(s => s.Trim()).ToList();
        values.Add((ls[0], ls[1].Split(' ').Select(s => s.Trim()).ToList()));
      });

      return values;

    }
    catch (IOException e)
    {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
      return [];
    }
    catch (Exception e)
    {
      Console.WriteLine("Table wasn't setup correctly:");
      Console.WriteLine(e.Message);
      return [];
    }
  }

  public IStateFactory GetNextStateByAction(string action)
  {
    return actionToState[action];
  }

  public (string, int) GetNextAction(string currentToken, int currentState)
  {
    return actions[currentState][currentToken];
  }

  public (string, List<string>) GetRuleByNum(int num)
  {
    return grammar[num];
  }

  public int GetStateTransition(string token, int currentState)
  {
    return transitions[currentState][token];
  }

}