public class ElevatorController
{
  private readonly List<Evaluator> _elevators;
  private List<List<int>> _requests;

  public ElevatorController(int numFloors, List<(string, int)> elevatorsInfo)
  {
    _elevators = [];
    elevatorsInfo.ForEach((info) =>
    {
      _elevators.Add(new Evaluator(new Elevator(info.Item2, info.Item1), numFloors));
    });
    _requests = [];
  }

  public void DistributeAndExecuteRequests(List<List<int>> requests)
  {
    _requests = [.. requests];
    _requests.ForEach((request) => {
      Evaluator elevator1 = FindClosestElevator(request.First());

      request.ForEach((reqFloor) => {
        elevator1.GetElevator().TargetFloor = reqFloor;
        IState? state = new StartState(elevator1);
        
        while (state != null)
        {
          state = state.Next();
        }
      });
    });
  }

  private Evaluator FindClosestElevator(int targetFloor)
  {
    return _elevators
      .OrderBy(e => Math.Abs(e.GetElevator().CurrentFloor - targetFloor))
      .First();
  }
}
