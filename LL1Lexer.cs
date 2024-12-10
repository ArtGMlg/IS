using System.Text.Json;
using Helpers;

public class LL1Lexer
{
  private readonly Dictionary<string, Dictionary<string, string>> table;
  private readonly Dictionary<string, IStateFactory> factories = [];

  public LL1Lexer(string tablePath, string grammarPath)
  {
    table = LoadUpTable(tablePath);
    LoadUpGrammar(grammarPath);
  }
  private Dictionary<string, Dictionary<string, string>> LoadUpTable(string path)
  {
    try
    {
      using StreamReader reader = new(path);

      string json = reader.ReadToEnd();

      var decodedJson = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json);

      decodedJson.EnsureNotNull(() => throw new Exception("Unable to decode table!"));

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

  private void LoadUpGrammar(string path)
  {
    try
    {
      using StreamReader reader = new(path);

      string json = reader.ReadToEnd();

      HashSet<string> terminals = [];
      HashSet<string> nonTerminals = [];

      List<List<string>> rules = [];

      json.Split('\n').Select(l => l.Trim()).ToList().ForEach(s =>
      {
        rules.Add(s.Split("->").Select(l => l.Trim()).ToList());
      });

      rules.Select(r => r[0]).ToList().ForEach(nt => nonTerminals.Add(nt));

      rules.Select(r => r[1]).ToList().ForEach(p => p.Split(' ').Where(s => !nonTerminals.Contains(s)).ToList().ForEach(t => terminals.Add(t)));

      var terminalFactory = new TerminalStateFactory();
      var nonTerminalFactory = new NonTerminalStateFactory();

      terminals.ToList().ForEach(t => factories.Add(t, terminalFactory));
      nonTerminals.ToList().ForEach(n => factories.Add(n, nonTerminalFactory));
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

  public IStateFactory? TokenToState(string token)
  {
    factories.TryGetValue(token, out var stateFactory);
    return stateFactory;
  }

  public string GetProduction(string top, string token)
  {
    table.TryGetValue(top, out var rules);
    rules.EnsureNotNull(() =>
      throw new Exception($"Table doesn't contain production for {top}")
    );
    rules?.ContainsKey(token).EnsureTrue(() =>
      throw new Exception($"No rule for {top} with token {token}")
    );
    return table[top][token];
  }

  public void IsTokenTerminal(string token, List<string> tokens)
  {
    tokens.FirstOrDefault()?.Equals(token).EnsureTrue(() =>
      throw new Exception($"Expected {token}, but found {tokens.First()}")
    );
  }
}