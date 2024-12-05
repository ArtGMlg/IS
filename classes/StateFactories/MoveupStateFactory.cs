
public class MoveUpStateFactory : IStateFactory
{
  public IState CreateState(Evaluator evaluator)
  {
    return new MoveUpState(evaluator);
  }
}
