public class StartState : IState
{
  private readonly Elevator elevator;
  private readonly Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions;

  public StartState(Elevator _elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> _transitions)
  {
    elevator = _elevator;
    transitions = _transitions;
  }
  public IState Next()
  {
    elevator.CloseDoors();
    elevator.ComputeNextCommand();
    return new State(elevator, transitions);
  }
}