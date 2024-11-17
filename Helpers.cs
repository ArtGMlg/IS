namespace Helpers
{
  public class SkipOrThrow
  {
    public static void CheckKnownToken(Dictionary<string, Dictionary<string, string>> _table, string _top, string token)
    {
      if (!(_table.ContainsKey(_top) && _table[_top].ContainsKey(token)))
      {
        throw new Exception($"No rule for {_top} with token {token}");
      }
    }

    public static void IsTokenTerminal(string _top, List<string> _tokens)
    {
      if (_top != _tokens.First())
      {
        throw new Exception($"Expected {_top}, but found {_tokens.First()}");
      }
    }

    public static void NoTokensShouldLeft(List<string> _tokens)
    {
      if (_tokens.Count > 0)
      {
        throw new Exception($"Input not fully consumed: Remaining {_tokens.Aggregate((res, next) => res + ' ' + next)}");
      }
    }

    public static void NotNullable(object? obj)
    {
      if (obj == null)
      {
        throw new Exception("The given object should not be nullable");
      }
    }
  }
}
