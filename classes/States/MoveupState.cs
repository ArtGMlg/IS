public class MoveUpState : IState
{
  private readonly Evaluator evaluator;

  public MoveUpState(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    evaluator.GetElevator().MoveUp();
    return new State(evaluator);
  }
}
