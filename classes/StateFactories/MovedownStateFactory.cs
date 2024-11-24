
public class MoveDownStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new MoveDownState(elevator, transitions);
  }
}
