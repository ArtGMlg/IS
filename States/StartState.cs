using System.Diagnostics.CodeAnalysis;
using Helpers;

public class StartState : IState
{
  private readonly Stack<string> _stack;
  private readonly List<string> _tokens;
  private readonly Dictionary<string, Dictionary<string, string>> _table;
  private readonly Dictionary<string, IStateFactory> _factories;

  public StartState(
    Stack<string> stack,
    List<string> tokens,
    Dictionary<string, Dictionary<string, string>> table,
    Dictionary<string, IStateFactory> factories
  )
  {
    _stack = stack;
    _tokens = tokens;
    _table = table;

    _factories = factories;
  }

  public IState? Next()
  {
    if (_stack.Count == 0)
    {
      return _tokens.Count == 0 ? new AcceptState() : new ErrorState($"Input not fully consumed: Remaining {_tokens.Aggregate((res, next) => res + ' ' + next)}");
    }

    try
    {
      string top = _stack.Pop();

      _factories.TryGetValue(top, out var factory);

      SkipOrThrow.NotNullable(factory);

      return factory?.CreateState(top, _stack, _tokens, _factories);
    }
    catch (InvalidOperationException)
    {
      return new FinalState(_tokens);
    }
    catch (Exception err)
    {
      return new ErrorState(err.Message);
    }
  }
}
