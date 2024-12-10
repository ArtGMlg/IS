
public class ReduceStateFactory : IStateFactory
{
  public IState CreateState(LR1Lexer lexer, List<string> tokens, Stack<int> statesStack, Stack<string> tokensStack, int n)
  {
    return new ReduceState(lexer, tokens, statesStack, tokensStack, n);
  }
}
