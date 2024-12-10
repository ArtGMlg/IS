public interface IStateFactory
{
  IState CreateState(
    LR1Lexer lexer,
    List<string> tokens,
    Stack<int> statesStack,
    Stack<string> tokensStack,
    int n
  );
}
