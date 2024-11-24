
public class MoveUpStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new MoveUpState(elevator, transitions);
  }
}
