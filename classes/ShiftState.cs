public class ShiftState : IState
{
  private readonly LR1Lexer lexer;
  private readonly List<string> tokens;
  private readonly Stack<int> statesStack;
  private readonly Stack<string> tokensStack;
  private readonly int n;

  public ShiftState(
    LR1Lexer lexer,
    List<string> tokens,
    Stack<int> statesStack,
    Stack<string> tokensStack,
    int n
  )
  {
    this.lexer = lexer;
    this.tokens = tokens;
    this.statesStack = statesStack;
    this.tokensStack = tokensStack;
    this.n = n;
  }
  public IState Next()
  {
    tokensStack.Push(tokens.First());
    statesStack.Push(n);
    try
    {
      (string, int) next = lexer.GetNextAction(tokens[1], statesStack.Peek());
      return lexer.GetNextStateByAction(next.Item1).CreateState(lexer, tokens[1..], statesStack, tokensStack, next.Item2);
    }
    catch (System.Exception)
    {
      return new ErrorState($"Shift error: n: {n}");
    }
  }
}