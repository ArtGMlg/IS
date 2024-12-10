public class ReduceState : IState
{
  private readonly LR1Lexer lexer;
  private readonly List<string> tokens;
  private readonly Stack<int> statesStack;
  private readonly Stack<string> tokensStack;
  private readonly int rule;

  public ReduceState(
    LR1Lexer lexer,
    List<string> tokens,
    Stack<int> statesStack,
    Stack<string> tokensStack,
    int rule
  )
  {
    this.lexer = lexer;
    this.tokens = tokens;
    this.statesStack = statesStack;
    this.tokensStack = tokensStack;
    this.rule = rule;
  }
  public IState Next()
  {
    (string, List<string>) grammarRule = lexer.GetRuleByNum(rule);
    if (rule == 2)
    {
      var a = 10;
    }
    grammarRule.Item2.Where(s => s != "''").ToList().ForEach((rm) =>
    {
      statesStack.Pop();
      tokensStack.Pop();
    });
    tokensStack.Push(grammarRule.Item1);
    try
    {
      statesStack.Push(lexer.GetStateTransition(tokensStack.Peek(), statesStack.Peek()));
    }
    catch (KeyNotFoundException)
    {
      return new ErrorState($"No transition for {tokensStack.Peek()} in state {statesStack.Peek()}");
    }
    (string, int) next = lexer.GetNextAction(tokens[0], statesStack.Peek());
    return lexer.GetNextStateByAction(next.Item1).CreateState(lexer, tokens, statesStack, tokensStack, next.Item2);
  }
}
