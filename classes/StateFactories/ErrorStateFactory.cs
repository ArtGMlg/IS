
public class ErrorStateFactory : IStateFactory
{
  public IState CreateState(Evaluator evaluator)
  {
    return new ErrorState(evaluator);
  }
}
