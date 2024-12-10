public class ErrorState : IState
{
  readonly string msg;
  public ErrorState(string msg)
  {
    this.msg = msg;
  }

  public IState? Next()
  {
    System.Console.WriteLine(msg);
    return null;
  }
}
