public class NonTerminalStateFactory : IStateFactory
{
  public IState CreateState(LL1Lexer lexer, string symbol, Stack<string> stack, List<string> tokens)
  {
    return new NonTerminalState(lexer, stack, tokens, symbol);
  }
}
