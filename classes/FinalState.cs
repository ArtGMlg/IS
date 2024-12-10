public class FinalState : IState
{
  public IState? Next()
  {
    System.Console.WriteLine("Input parsed successfully");
    return null;
  }
}
