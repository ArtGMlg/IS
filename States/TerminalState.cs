using Helpers;

public class TerminalState : IState
{
  private LL1Lexer _lexer;
  private readonly Stack<string> _stack;
  private readonly List<string> _tokens;
  private readonly string _top;

  public TerminalState(
    LL1Lexer lexer,
    Stack<string> stack,
    List<string> tokens,
    string top
  )
  {
    _lexer = lexer;
    _stack = stack;
    _tokens = tokens;
    _top = top;
  }

  public IState? Next()
  {
    try
    {
      _lexer.IsTokenTerminal(_top, _tokens);
    
      return new StartState(_lexer, _stack, _tokens[1..]);
    }
    catch (Exception err)
    {
      return new ErrorState(err.Message);
    }
  }
}
