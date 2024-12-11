public class StartState : IState
{
  private readonly LR1Lexer lexer;
  private readonly List<string> tokens;
  private readonly Stack<int> statesStack;
  private readonly Stack<string> tokensStack;

  public StartState(
    LR1Lexer lexer,
    List<string> tokens,
    Stack<int> statesStack,
    Stack<string> tokensStack
  )
  {
    this.lexer = lexer;
    this.tokens = tokens;
    this.statesStack = statesStack;
    this.tokensStack = tokensStack;
  }
  public IState Next()
  {
    int currentState = statesStack.Peek();
    string firstToken = tokens.First();
    (string, int) next = lexer.GetNextAction(firstToken, currentState);
    return lexer.GetNextStateByAction(next.Item1).CreateState(lexer, tokens, statesStack, tokensStack, next.Item2);
  }
}
