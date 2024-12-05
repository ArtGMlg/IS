public class OpenDoorsState : IState
{
  private readonly Evaluator evaluator;

  public OpenDoorsState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    evaluator.GetElevator().OpenDoors();
    return new State(evaluator);
  }
}
