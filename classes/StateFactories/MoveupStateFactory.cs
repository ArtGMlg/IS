
public class MoveupStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new MoveupState(elevator, transitions);
  }
}