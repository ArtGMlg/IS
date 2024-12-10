public class MoveDownState : IState
{
  private readonly Evaluator evaluator;

  public MoveDownState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    evaluator.GetElevator().MoveDown();
    return new State(evaluator);
  }
}
