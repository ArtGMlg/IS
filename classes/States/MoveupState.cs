public class MoveUpState : IState
{
  private readonly Evaluator evaluator;

  public MoveUpState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    evaluator.GetElevator().CurrentFloor += 1;
    Console.WriteLine($"{evaluator.GetElevator().Name}: The elevator went up from floor {evaluator.GetElevator().CurrentFloor - 1} to floor {evaluator.GetElevator().CurrentFloor}");
    return new State(evaluator);
  }
}
