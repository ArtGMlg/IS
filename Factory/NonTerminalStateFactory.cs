public class NonTerminalStateFactory : IStateFactory
{
  private readonly Dictionary<string, Dictionary<string, string>> table;

  public NonTerminalStateFactory(Dictionary<string, Dictionary<string, string>> table)
  {
    this.table = table;
  }

  public IState CreateState(string symbol, Stack<string> stack, List<string> tokens, Dictionary<string, IStateFactory> factories)
  {
    return new NonTerminalState(stack, tokens, table, symbol, factories);
  }
}
