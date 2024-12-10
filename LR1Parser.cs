public class LR1Parser
{
  readonly LR1Lexer lexer;
  readonly Stack<int> statesStack = [];
  readonly Stack<string> tokensStack = [];

  public LR1Parser(LR1Lexer lexer)
  {
    this.lexer = lexer;
  }

  public void Parse(string input)
  {
    statesStack.Clear();
    tokensStack.Clear();

    statesStack.Push(0);

    List<string> tokens = [.. input.Split(' ')];
    tokens.Add("$");

    IState? state = new StartState(lexer, tokens, statesStack, tokensStack);

    while (state != null)
    {
      state = state.Next();
    }
  }
}
