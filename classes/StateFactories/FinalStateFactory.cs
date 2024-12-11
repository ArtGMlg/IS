
public class FinalStateFactory : IStateFactory
{
  public IState CreateState(Evaluator evaluator)
  {
    return new FinalState();
  }
}
