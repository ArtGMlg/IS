using Helpers;

public class NonTerminalState : IState
{
  private Stack<string> _stack;
  private readonly List<string> _tokens;
  private readonly Dictionary<string, Dictionary<string, string>> _table;
  private readonly string _top;

  Dictionary<string, IStateFactory> _factories;

  public NonTerminalState(
    Stack<string> stack,
    List<string> tokens,
    Dictionary<string, Dictionary<string, string>> table,
    string top,
    Dictionary<string, IStateFactory> factories
  )
  {
    _stack = stack;
    _tokens = tokens;
    _table = table;
    _top = top;
    _factories = factories;
  }

  public IState? Next()
  {
    var token = _tokens.FirstOrDefault("ε");

    SkipOrThrow.CheckKnownToken(_table, _top, token);

    string production = _table[_top][token];
    string[] symbols = production.Split(' ');

    for (int i = symbols.Length - 1; i >= 0; i--)
    {
      _stack.Push(symbols[i]);
    }

    _stack = new Stack<string>(_stack.Where((s) => s != "ε").Reverse());

    return new StartState(_stack, _tokens, _table, _factories);
  }
}
