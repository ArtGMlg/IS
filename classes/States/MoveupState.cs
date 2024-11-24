public class MoveUpState : IState
{
  private readonly Elevator elevator;
  private readonly Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions;

  public MoveUpState(Elevator _elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> _transitions)
  {
    elevator = _elevator;
    transitions = _transitions;
  }
  public IState Next()
  {
    elevator.CurrentFloor += 1;
    elevator.ComputeNextCommand();
    Console.WriteLine($"{elevator.Name}: The elevator went up from floor {elevator.CurrentFloor - 1} to floor {elevator.CurrentFloor}");
    return new State(elevator, transitions);
  }
}
