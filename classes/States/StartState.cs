public class StartState : IState
{
  private readonly Evaluator evaluator;

  public StartState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    evaluator.GetElevator().CloseDoors();
    return new State(evaluator);
  }
}
