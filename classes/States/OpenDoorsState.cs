public class OpenDoorsState : IState
{
  private readonly Elevator elevator;
  private readonly Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions;

  public OpenDoorsState(Elevator _elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> _transitions)
  {
    elevator = _elevator;
    transitions = _transitions;
  }
  public IState Next()
  {
    elevator.OpenDoors();
    return new State(elevator, transitions);
  }
}