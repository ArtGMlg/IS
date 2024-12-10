public class Elevator
{
  public int CurrentFloor { get; set; }
  public int TargetFloor { get; set; }
  public readonly string Name;
  public int IsDoorOpen = 1;

  public Elevator(int currentFloor, string name)
  {
    CurrentFloor = currentFloor;
    Name = name;
  }
  public void OpenDoors()
  {
    Console.WriteLine($"{Name}: The elevator opened the doors on {CurrentFloor} floor");
    IsDoorOpen = 1;
  }
  public void CloseDoors()
  {
    Console.WriteLine($"{Name}: The elevator closed the doors on {CurrentFloor} floor");
    IsDoorOpen = 0;
  }
  public void MoveUp()
  {
    CurrentFloor = CurrentFloor + 1;
    Console.WriteLine($"{Name}: The elevator went up from floor {CurrentFloor - 1} to floor {CurrentFloor}");
  }
  public void MoveDown()
  {
    CurrentFloor = CurrentFloor - 1;
    Console.WriteLine($"{Name}: The elevator went down from floor {CurrentFloor + 1} to floor {CurrentFloor}");
  }
}
