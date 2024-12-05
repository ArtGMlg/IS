public class ErrorState : IState
{
  private readonly Evaluator evaluator;

  public ErrorState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    throw new InvalidOperationException($"{evaluator.GetElevator().Name}: Cannot move to the next floor!\nThe elevator failed to execute command while moving from floor {evaluator.GetElevator().CurrentFloor} to floor {evaluator.GetElevator().TargetFloor}");
  }
}
