public class ErrorState : IState
{
  private readonly string _message;

  public ErrorState(string message)
  {
    _message = message;
  }

  public IState? Next()
  {
    Console.WriteLine($"Parsing failed: {_message}");
    return null;
  }
}
