class Program
{
  static void Main(string[] args)
  {
    LL1Lexer lexer = new(args[0], args[1]);
    LL1Parser parser = new(lexer);

    try
    {
      using StreamReader reader = new(args[2]);

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
