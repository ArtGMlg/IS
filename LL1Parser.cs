using System.Text.Json;
using Helpers;

public class LL1Parser
{
  private readonly LL1Lexer _lexer;
  private readonly Stack<string> stack = [];

  public LL1Parser(LL1Lexer lexer)
  {
    _lexer = lexer;
  }

  public void Parse(string input)
  {
    stack.Clear();
    stack.Push("S");

    List<string> tokens = [.. input.Split(' ')];

    IState? state = new StartState(_lexer, stack, tokens);

    while (state != null)
    {
      state = state.Next();
    }
  }

}
