using Helpers;

public class FinalState : IState
{
  List<string> _tokens;

  public FinalState (List<string> tokens)
  {
    _tokens = tokens;
  }
  public IState? Next()
  {
    SkipOrThrow.NoTokensShouldLeft(_tokens);
    return new AcceptState();
  }
}