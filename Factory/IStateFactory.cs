public interface IStateFactory
{
  IState CreateState(LL1Lexer lexer, string symbol, Stack<string> stack, List<string> tokens);
}
