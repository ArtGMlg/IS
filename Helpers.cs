namespace Helpers
{
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
