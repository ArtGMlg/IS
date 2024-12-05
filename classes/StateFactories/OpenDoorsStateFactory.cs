
public class OpenDoorsStateFactory : IStateFactory
{
  public IState CreateState(Evaluator evaluator)
  {
    return new OpenDoorsState(evaluator);
  }
}
