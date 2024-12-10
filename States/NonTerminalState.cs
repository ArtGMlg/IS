using Helpers;

public class NonTerminalState : IState
{
  private readonly LL1Lexer _lexer;
  private readonly Stack<string> _stack;
  private readonly List<string> _tokens;
  private readonly string _top;

  public NonTerminalState(
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
      var token = _tokens.FirstOrDefault("ε");

      string production = _lexer.GetProduction(_top, token);
      production.Split(' ').Where(s => s != "ε").Reverse().ToList().ForEach(_stack.Push);

      return new StartState(_lexer, _stack, _tokens);
    }
    catch (Exception err)
    {
      return new ErrorState(err.Message);
    }
  }
}
