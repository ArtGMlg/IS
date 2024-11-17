using Helpers;

public class TerminalState : IState
{
  private readonly Stack<string> _stack;
  private readonly List<string> _tokens;
  private readonly string _top;
  Dictionary<string, Dictionary<string, string>> _table;
  Dictionary<string, IStateFactory> _factories;

  public TerminalState(
    Stack<string> stack,
    List<string> tokens,
    Dictionary<string, Dictionary<string, string>> table,
    string top,
    Dictionary<string, IStateFactory> factories
  )
  {
    _stack = stack;
    _tokens = tokens;
    _top = top;
    _table = table;
    _factories = factories;
  }

  public IState? Next()
  {
    SkipOrThrow.IsTokenTerminal(_top, _tokens);
    
    return new StartState(_stack, _tokens[1..], _table, _factories);
  }
}
