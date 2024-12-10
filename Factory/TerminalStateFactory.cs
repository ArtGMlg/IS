public class TerminalStateFactory : IStateFactory
{
  public IState CreateState(LL1Lexer lexer, string symbol, Stack<string> stack, List<string> tokens)
  {
    return new TerminalState(lexer, stack, tokens, symbol);
  }
}
