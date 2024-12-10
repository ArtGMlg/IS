class Program
{
  static void Main(string[] args)
  {
    LR1Lexer lexer = new("D:\\Новая папка\\LR1\\LR1Actions.json", "D:\\Новая папка\\LR1\\LR1Transitions.json", "D:\\Новая папка\\LR1\\grammar.txt");
    LR1Parser parser = new(lexer);

    try
    {
      using StreamReader reader = new("D:\\Новая папка\\LR1\\input.txt");

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
