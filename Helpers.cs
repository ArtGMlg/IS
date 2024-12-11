namespace Helpers
{
  public class SkipOrThrow
  {
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
