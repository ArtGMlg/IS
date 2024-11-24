public class FinalState : IState
{
  private readonly Elevator elevator;
  private readonly Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions;

  public FinalState(Elevator _elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> _transitions)
  {
    elevator = _elevator;
    transitions = _transitions;
  }
  public IState? Next()
  {
    return null;
  }
}
