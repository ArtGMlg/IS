try
{
  using StreamReader reader = new("cases.txt");

  string text = reader.ReadToEnd();

  List<string> textSplit = text.Split('@').ToList();

  int numFloors = Convert.ToInt32(textSplit[0]);

  List<(string, int)> elevatorsInfo = textSplit[1]
    .Trim()
    .Split('\n')
    .Select((s) => s.Split(' '))
    .Select((sl) => (sl[0], Convert.ToInt32(sl[1])))
    .ToList();

  List<List<int>> requests = textSplit[2]
    .Trim()
    .Split('\n')
    .Select((s) => s.Split(' '))
    .Select((sl) => sl.ToList().Select((f) => Convert.ToInt32(f)).ToList())
    .ToList();

  ElevatorController elevatorController = new(numFloors, elevatorsInfo);

  elevatorController.DistributeAndExecuteRequests(requests);
}
catch (IOException e)
{
  Console.WriteLine("The file could not be read:");
  Console.WriteLine(e.Message);
}
