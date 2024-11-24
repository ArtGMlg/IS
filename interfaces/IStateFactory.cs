public interface IStateFactory
{
  IState CreateState(Elevator elevator, Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions);
}
