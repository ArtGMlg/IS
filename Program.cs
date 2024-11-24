List<List<int>> requests = [[1, 10], [3, 11], [2, 6], [9, 4], [8, 9], [11, 5], [7, 4], [6, 3], [10, 11], [12, 8], [5, 2], [1, 7], [4, 12], [6, 8], [3, 5]];

ElevatorController elevatorController = new(12, [("Elevator 1", 6), ("Elevator 2", 4)]);

elevatorController.DistributeAndExecuteRequests(requests);
