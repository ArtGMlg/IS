public class State : IState
{
  private readonly Evaluator evaluator;

  public State(Evaluator _evaluator)
  {
    evaluator = _evaluator;
  }
  public IState Next()
  {
    return evaluator.Decide();
  }
}
