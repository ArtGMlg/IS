using Helpers;

public class StartState : IState
{
  private LL1Lexer _lexer;
  private readonly Stack<string> _stack;
  private readonly List<string> _tokens;

  public StartState(
    LL1Lexer lexer,
    Stack<string> stack,
    List<string> tokens
  )
  {
    _lexer = lexer;
    _stack = stack;
    _tokens = tokens;
  }

  public IState? Next()
  {
    try
    {
      string top = _stack.Pop();

      var factory = _lexer.TokenToState(top);

      factory.EnsureNotNull(() => throw new Exception($"Unable to find state for token {top}"));

      return factory?.CreateState(_lexer, top, _stack, _tokens);
    }
    catch (InvalidOperationException)
    {
      return new FinalState(_tokens);
    }
    catch (Exception err)
    {
      return new ErrorState(err.Message);
    }
  }
}
