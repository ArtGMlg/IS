
public class OpenDoorsStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new OpenDoorsState(elevator, transitions);
  }
}
