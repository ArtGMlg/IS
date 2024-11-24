public class ElevatorController
{
  private readonly List<Elevator> _elevators;
  private List<List<int>> _requests;

  public ElevatorController(int numFloors, List<(string, int)> elevatorsInfo)
  {
    _elevators = [];
    elevatorsInfo.ForEach((info) =>
    {
      _elevators.Add(new Elevator(info.Item2, numFloors, info.Item1));
    });
    _requests = [];
  }

  public void DistributeAndExecuteRequests(List<List<int>> requests)
  {
    _requests = [.. requests];
    while (_requests.Count != 0)
    {
      List<int> request = _requests.First();
      Elevator? elevator1 = FindClosestElevator(request.First());

      elevator1?.Proceed(request);

      _requests = _requests[1..];
    }
  }

  private Elevator FindClosestElevator(int targetFloor)
  {
    return _elevators
      .OrderBy(e => Math.Abs(e.CurrentFloor - targetFloor))
      .First();
  }
}
