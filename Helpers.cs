namespace Helpers
{
  public class SkipOrThrow
  {
    public static void CheckKnownToken(Dictionary<string, Dictionary<string, string>> _table, string _top, string token)
    {
      _table.TryGetValue(_top, out var rules);
      rules?.ContainsKey(token).EnsureTrue(() =>
        throw new Exception($"No rule for {_top} with token {token}")
      );
    }

    public static void IsTokenTerminal(string _top, List<string> _tokens)
    {
      _tokens.FirstOrDefault()?.Equals(_top).EnsureTrue(() =>
        throw new Exception($"Expected {_top}, but found {_tokens.First()}")
      );
    }

    public static void NoTokensShouldLeft(List<string> _tokens)
    {
      _tokens.Any().EnsureFalse(() =>
        throw new Exception($"Input not fully consumed: Remaining {_tokens.Aggregate((res, next) => res + ' ' + next)}")
      );
    }

    public static void NotNullable(object? obj)
    {
      obj.EnsureNotNull(() =>
        throw new Exception("The given object should not be nullable")
      );
    }
  }

  public static class EnsureExtensions
  {
    public static void EnsureTrue(this bool condition, Action onFailure)
    {
      if (!condition) onFailure();
    }

    public static void EnsureFalse(this bool condition, Action onFailure)
    {
      if (condition) onFailure();
    }

    public static void EnsureNotNull(this object? obj, Action onFailure)
    {
      if (obj == null) onFailure();
    }
  }
}
