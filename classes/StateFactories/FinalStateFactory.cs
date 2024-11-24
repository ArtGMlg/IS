
public class FinalStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new FinalState(elevator, transitions);
  }
}