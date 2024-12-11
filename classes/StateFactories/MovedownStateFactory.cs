
public class MoveDownStateFactory : IStateFactory
{
  public IState CreateState(Evaluator evaluator)
  {
    return new MoveDownState(evaluator);
  }
}
