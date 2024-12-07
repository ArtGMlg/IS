class Program
{
  static void Main(string[] args)
  {
    LL1Parser parser = new();

    parser.LoadUpTable(args[0]);

    try
    {
      using StreamReader reader = new(args[1]);

      string text = reader.ReadToEnd();

      List<string> textSplit = text.Split('\n').ToList();

      textSplit.ForEach(parser.Parse);
    }
    catch (IOException e)
    {
      Console.WriteLine("The file could not be read:");
      Console.WriteLine(e.Message);
    }
  }
}
