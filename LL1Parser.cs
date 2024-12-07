using System.Text.Json;
using Helpers;

public class LL1Parser
{
  private Dictionary<string, Dictionary<string, string>> table;
  private readonly Stack<string> stack;
  private readonly HashSet<string> terminals;
  private readonly List<string> nonTerminals;
  private readonly Dictionary<string, IStateFactory> factories = [];

  public LL1Parser()
  {
    table = [];
    stack = new Stack<string>();
    terminals = [];
    nonTerminals = [];
  }

  public void LoadUpTable (string path)
  {
    try
    {
      using StreamReader reader = new(path);

      string json = reader.ReadToEnd();

      var decodedJson = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

      SkipOrThrow.NotNullable(decodedJson);

      table = decodedJson;
      
      var terminalFactory = new TerminalStateFactory(table);
      var nonTerminalFactory = new NonTerminalStateFactory(table);

      table.Keys.ToList().ForEach(nonTerminal => {
        factories.TryAdd(nonTerminal, nonTerminalFactory);
      });

      table.Values.ToList().ForEach((ruleSet) =>
      {
        ruleSet.Keys.ToList().ForEach((terminal) => {
          factories.TryAdd(terminal, terminalFactory);
        });

        ruleSet.Values.ToList().ForEach((r) => {
          r.Split(' ').ToList().ForEach((s) =>
          {
            if (!nonTerminals.Contains(s))
            {
              factories.TryAdd(s, terminalFactory);
            }
          });
        });
      });

      var a = 10;
      
    }
    catch (IOException e)
    {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
    }
    catch (Exception e)
    {
      Console.WriteLine("Table wasn't setup correctly:");
      Console.WriteLine(e.Message);
    }

  }

  public void Parse(string input)
  {
    stack.Clear();
    stack.Push("S");

    List<string> tokens = [.. input.Split(' ')];

    IState? state = new StartState(stack, tokens, table, factories);

    while (state != null)
    {
      state = state.Next();
    }
  }

}
