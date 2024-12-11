public class AcceptState : IState
{
  public IState? Next()
  {
    Console.WriteLine("Input parsed successfully.");
    return null;
  }
}
