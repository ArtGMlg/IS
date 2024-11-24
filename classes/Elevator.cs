public class Elevator
{
  public int CurrentFloor { get; set; }
  public int TargetFloor { get; set; }
  public ElevatorCommands CurrentCommand = ElevatorCommands.None;
  private Dictionary<int, ElevatorCommands> commandIdToCommand = [];
  Dictionary<int, Dictionary<ElevatorCommands, IStateFactory>> transitions = [];
  public readonly string Name;

  public Elevator(int currentFloor, int numFloors, string name)
  {
    CurrentFloor = currentFloor;
    commandIdToCommand[1] = ElevatorCommands.MoveDown;
    commandIdToCommand[-1] = ElevatorCommands.MoveUp;
    commandIdToCommand[0] = ElevatorCommands.OpenDoors;
    Name = name;

    for (int i = 2; i <= numFloors - 1; i++)
    {
      transitions[i] = new Dictionary<ElevatorCommands, IStateFactory>
      {
        { ElevatorCommands.MoveUp, new MoveupStateFactory() },
        { ElevatorCommands.MoveDown, new MovedownStateFactory() },
        { ElevatorCommands.OpenDoors, new OpenDoorsStateFactory() },
        { ElevatorCommands.None, new FinalStateFactory() },
      };
    }
    transitions[1] = new Dictionary<ElevatorCommands, IStateFactory>
    {
      { ElevatorCommands.MoveUp, new MoveupStateFactory() },
      { ElevatorCommands.MoveDown, new ErrorStateFactory() },
      { ElevatorCommands.OpenDoors, new OpenDoorsStateFactory() },
      { ElevatorCommands.None, new FinalStateFactory() },
    };
    transitions[numFloors] = new Dictionary<ElevatorCommands, IStateFactory>
    {
      { ElevatorCommands.MoveUp, new ErrorStateFactory() },
      { ElevatorCommands.MoveDown, new MovedownStateFactory() },
      { ElevatorCommands.OpenDoors, new OpenDoorsStateFactory() },
      { ElevatorCommands.None, new FinalStateFactory() },
    };
  }
  public void ComputeNextCommand()
  {
    CurrentCommand = commandIdToCommand[CurrentFloor.CompareTo(TargetFloor)];
  }
  public void OpenDoors()
  {
    Console.WriteLine($"{Name}: The elevator opened the doors on {CurrentFloor} floor");
    CurrentCommand = ElevatorCommands.None;
  }
  public void CloseDoors()
  {
    Console.WriteLine($"{Name}: The elevator closed the doors on {CurrentFloor} floor");
  }
  public void Proceed(List<int> request)
  {
    while (request.Count > 0)
    {
      TargetFloor = request.First();
      request = request[1..];
      IState? state = new StartState(this, transitions);

      while (state != null)
      {
        state = state.Next();
      }
    }
  }
}
