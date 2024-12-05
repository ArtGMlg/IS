public class Evaluator
{
  private Elevator elevator;
  private int numFloors;
  private Dictionary<int, Dictionary<(int, int), IStateFactory>> transitions = [];
  public Evaluator(Elevator elevator, int numFloors)
  {
    this.elevator = elevator;
    this.numFloors = numFloors;
    ProduceRules();
  }
  private void ProduceRules()
  {
    for (int i = 2; i <= numFloors - 1; i++)
    {
      transitions[i] = new Dictionary<(int, int), IStateFactory>
      {
        { (-1, 0), new MoveUpStateFactory() },
        { (1, 0), new MoveDownStateFactory() },
        { (0, 0), new OpenDoorsStateFactory() },
        { (0, 1), new FinalStateFactory() },
      };
    }
    transitions[1] = new Dictionary<(int, int), IStateFactory>
    {
      { (-1, 0), new MoveUpStateFactory() },
      { (1, 0), new ErrorStateFactory() },
      { (0, 0), new OpenDoorsStateFactory() },
      { (0, 1), new FinalStateFactory() },
    };
    transitions[numFloors] = new Dictionary<(int, int), IStateFactory>
    {
      { (-1, 0), new ErrorStateFactory() },
      { (1, 0), new MoveDownStateFactory() },
      { (0, 0), new OpenDoorsStateFactory() },
      { (0, 1), new FinalStateFactory() },
    };
  }
  public IState Decide()
  {
    return transitions[elevator.CurrentFloor][(elevator.CurrentFloor.CompareTo(elevator.TargetFloor), elevator.IsDoorOpen)].CreateState(this);
  }
  public Elevator GetElevator ()
  {
    return elevator;
  }
}
