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
    try
    {
      (_tokens.Count() == 0).EnsureTrue(() =>
        throw new Exception($"Input not fully consumed: Remaining {_tokens.Aggregate((res, next) => res + ' ' + next)}")
      );
      return new AcceptState();
    }
    catch (Exception err)
    {
      return new ErrorState(err.Message);
    }
  }
}
