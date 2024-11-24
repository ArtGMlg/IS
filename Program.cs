List<List<int>> requests = [
  [2, 15],
  [6, 10],
  [14, 3],
  [8, 1],
  [10, 5],
  [7, 13],
  [3, 9],
  [12, 2],
  [9, 4],
  [1, 16],
  [13, 6],
  [5, 8],
  [16, 0],
  [4, 14],
  [15, 11],
  [11, 3],
  [10, 12],
  [8, 5],
  [2, 9],
  [6, 16]
];

ElevatorController elevatorController = new(16, [("Elevator 1", 4), ("Elevator 2", 11)]);

elevatorController.DistributeAndExecuteRequests(requests);
