public class LL1Parser
{
  private readonly Dictionary<string, List<string>> grammar;
  private readonly Dictionary<string, HashSet<string>> first;
  private readonly Dictionary<string, HashSet<string>> follow;
  private readonly Dictionary<string, Dictionary<string, string>> table;
  private readonly Stack<string> stack;
  private readonly HashSet<string> terminals;
  private readonly List<string> nonTerminals;
  private readonly Dictionary<string, IStateFactory> factories = [];

  public LL1Parser()
  {
    grammar = [];
    first = [];
    follow = [];
    table = [];
    stack = new Stack<string>();
    terminals = [];
    nonTerminals = [];
  }

  public void DefineGrammar()
  {
    grammar["S"] = ["E"];
    grammar["E"] = ["T E'"];
    grammar["E'"] = ["|| T E'", "RelOp T", "ε"];
    grammar["T"] = ["F T'"];
    grammar["T'"] = ["&& F T'", "AddOp F T'", "MulOp F T'", "( Args )", "ε"];
    grammar["F"] = ["( E )", "n", "true", "false", "id"];
    grammar["Args"] = ["E Args'", "ε"];
    grammar["Args'"] = [", E Args'", "ε"];
    grammar["RelOp"] = ["==", "!=", "<", ">", "<=", ">="];
    grammar["AddOp"] = ["+", "-"];
    grammar["MulOp"] = ["*", "/"];

    grammar.Keys.ToList().ForEach(nonTerminals.Add);

    grammar.Values.ToList().ForEach((production) =>
    {
      production.ForEach((rule) =>
      {
        rule.Split(' ').ToList().ForEach((s) =>
        {
          if (!nonTerminals.Contains(s))
          {
            terminals.Add(s);
          }
        });
      });
    });

    ComputeFirst();
    foreach (var entry in first)
    {
      Console.Write($"FIRST({entry.Key}) = {{ ");
      foreach (var item in entry.Value)
      {
        Console.Write($"{item} ");
      }
      Console.WriteLine("}");
    }

    ComputeFollow();
    foreach (var entry in follow)
    {
      Console.Write($"FOLLOW({entry.Key}) = {{ ");
      foreach (var item in entry.Value)
      {
        Console.Write($"{item} ");
      }
      Console.WriteLine("}");
    }

    Buildtable();
    Console.WriteLine("Parsing Table:");
    foreach (var entry in table)
    {
      foreach (var terminal in entry.Value)
      {
        Console.WriteLine($"M[{entry.Key}, {terminal.Key}] = {terminal.Value}");
      }
    }
  }

  private void ComputeFirst()
  {
    grammar.Keys.ToList().ForEach((k) =>
    {
      first.Add(k, []);
    });

    bool changed = true;
    while (changed)
    {
      changed = false;

      foreach (var rule in grammar)
      {
        string A = rule.Key;
        foreach (var production in rule.Value)
        {
          string[] symbols = production.Split(' ');
          bool allEpsilon = true;

          foreach (var symbol in symbols)
          {
            if (!grammar.ContainsKey(symbol))
            {
              if (first[A].Add(symbol))
              {
                changed = true;
              }
              allEpsilon = false;
              break;
            }
            else
            {
              if (first.TryGetValue(symbol, out HashSet<string>? value))
              {
                foreach (var firstSymbol in value)
                {
                  if (firstSymbol != "ε" && first[A].Add(firstSymbol))
                  {
                    changed = true;
                  }
                }

                if (!value.Contains("ε"))
                {
                  allEpsilon = false;
                  break;
                }
              }
            }
          }

          if (allEpsilon)
          {
            if (first[A].Add("ε"))
            {
              changed = true;
            }
          }
        }
      }
    }
  }

  private void ComputeFollow()
  {
    grammar.Keys.ToList().ForEach((k) =>
    {
      follow.Add(k, []);
    });

    follow["S"].Add("$");

    bool changed = true;

    while (changed)
    {
      changed = false;
      grammar.ToList().ForEach((rule) =>
      {
        string A = rule.Key;

        rule.Value.ForEach((production) =>
        {
          List<string> symbols = [.. production.Split(' ')];

          for (int i = 0; i < symbols.Count; i++)
          {
            string B = symbols[i];

            if (grammar.ContainsKey(B))
            {
              List<string> gamma = symbols.Skip(i + 1).ToList();

              var firstOfGamma = First(gamma);
              bool hasEps = firstOfGamma.Remove("ε");

              if (firstOfGamma.Count != 0)
              {
                int prevCount = follow[B].Count;
                follow[B].UnionWith(firstOfGamma);
                if (follow[B].Count > prevCount) changed = true;
              }

              if (hasEps || gamma.Count == 0)
              {
                int prevCount = follow[B].Count;
                follow[B].UnionWith(follow[A]);
                if (follow[B].Count > prevCount) changed = true;
              }
            }
          }
        });
      });
    }
  }

  private HashSet<string> First(List<string> symbols)
  {
    var result = new HashSet<string>();

    foreach (var symbol in symbols)
    {
      if (!grammar.ContainsKey(symbol))
      {
        result.Add(symbol);
        break;
      }

      result.UnionWith(first[symbol]);

      if (!first[symbol].Contains("ε"))
      {
        break;
      }
    }

    return result;
  }

  private void Buildtable()
  {
    table.Clear();

    foreach (var rule in grammar)
    {
      string A = rule.Key;
      foreach (var production in rule.Value)
      {
        List<string> symbols = [.. production.Split(' ')];

        var firstOfProduction = First(symbols);

        foreach (var terminal in firstOfProduction)
        {
          if (terminal != "ε")
          {
            if (!table.TryGetValue(A, out Dictionary<string, string>? value))
            {
              value = [];
              table[A] = value;
            }

            value[terminal] = production;
          }
        }

        if (firstOfProduction.Contains("ε"))
        {
          foreach (var followSymbol in follow[A])
          {
            if (!table.TryGetValue(A, out Dictionary<string, string>? value))
            {
              value = [];
              table[A] = value;
            }

            value[followSymbol] = production;
          }
          table[A]["ε"] = "ε";
        }
      }
    }

    var terminalFactory = new TerminalStateFactory(table);
    foreach (var terminal in terminals)
    {
      factories[terminal] = terminalFactory;
    }

    var nonTerminalFactory = new NonTerminalStateFactory(table);
    foreach (var nonTerminal in nonTerminals)
    {
      factories[nonTerminal] = nonTerminalFactory;
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

class Program
{
  static void Main(string[] args)
  {
    LL1Parser parser = new LL1Parser();
    parser.DefineGrammar();

    string input1 = "n + id * n";
    parser.Parse(input1);

    string input2 = "( id < n ) && ( true || false )";
    parser.Parse(input2);

    string input3 = "id ( id , id + n , true )";
    parser.Parse(input3);

    string input4 = "( n <= id ) == true";
    parser.Parse(input4);

    string input5 = "( ( n + n ) * n - n ) == n";
    parser.Parse(input5);
  }
}
