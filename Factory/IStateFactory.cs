public interface IStateFactory
{
  IState CreateState(string symbol, Stack<string> stack, List<string> tokens, Dictionary<string, IStateFactory> factories);
}
