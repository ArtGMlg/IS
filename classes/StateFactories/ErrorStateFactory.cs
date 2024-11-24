
public class ErrorStateFactory : IStateFactory
{
  public IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions)
  {
    return new ErrorState(elevator, transitions);
  }
}