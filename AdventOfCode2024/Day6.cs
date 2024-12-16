using System.IO;

Console.WriteLine("Advent of Code - Day 6");

var input = await File.ReadAllLinesAsync(@"C:\Users\abircan\source\repos\adventofcode\day6.txt");
var map = new string[input[0].Length, input.Count()];
var visited = new HashSet<(int x, int y)>();
(int x, int y) position = (0, 0);
var direction = "UP";

for (int i = 0; i < map.GetLength(0); i++) {
    for (int j = 0; j < map.GetLength(1); j++) {
        map[i, j] = input[i][j].ToString();

        if (map[i, j] == "^") {
            position = (i, j);
        }
    }
}

SolvePartOne(map, new HashSet<(int, int)>(visited), direction, position, true);
SolvePartTwo(map, new HashSet<(int, int)>(visited), direction, position);

private bool SolvePartTwo(string[,] map, HashSet<(int, int)> visited, string direction, (int x, int y) position) {
    var infiniteLoops = 0;
    for (int i = 0; i < map.GetLength(0); i++) {
        for (int j = 0; j < map.GetLength(1); j++) {
            if (map[i, j] != "^" && map[i, j] != "#") {
                var newMap = map.Clone() as string[,];
                newMap[i, j] = "#";
                var solved = SolvePartOne(newMap, new HashSet<(int, int)>(visited), direction, position, false);

                if (!solved) {
                    infiniteLoops++;
                }
            }
        }
    }

    Console.WriteLine($"Infinite loops: {infiniteLoops}");
    return true;
}

private bool SolvePartOne(string[,] map, HashSet<(int, int)> visited, string direction, (int x, int y) position, bool isPartOne) {
    var solved = false;
    var maxSteps = 100000;
    var steps = 0;

    while (!solved) {
        if (steps == maxSteps){
            break;
        }
        else {
            steps++;
        }

        (int, int) nextPosition = GetNextPosition(direction, position);
        if (IsNextPositionWalkable(map, nextPosition)) {
            visited.Add(position);
            if (IsNextPositionOutsideMap(map, nextPosition)){
                solved = true;
            } 
            else {
                position = nextPosition;
            }
        }
        else {
            direction = ChangeDirection(direction);
        }
    }

    if (solved && isPartOne) {
        Console.WriteLine($"Visited positions: {visited.Count}");
    }
    return solved;
}

private (int, int) GetNextPosition(string direction, (int x, int y) currentPosition) {
    if (direction == "UP") {
        return (currentPosition.x - 1, currentPosition.y);
    } 
    else if (direction == "DOWN") {
        return (currentPosition.x + 1, currentPosition.y);
    }
    else if (direction == "RIGHT") {
        return (currentPosition.x, currentPosition.y + 1);
    }
    else if (direction == "LEFT") {
        return (currentPosition.x, currentPosition.y - 1);
    }
    else {
        throw new Exception("Unknown position");
    }
}

private bool IsNextPositionWalkable(string[,] map, (int x, int y) nextPosition) {
    return IsNextPositionOutsideMap(map, nextPosition) || map[nextPosition.x, nextPosition.y] != "#";
}

private bool IsNextPositionOutsideMap(string[,] map, (int x, int y) nextPosition) {
    return nextPosition.x >= map.GetLength(0) || nextPosition.x < 0 || nextPosition.y >= map.GetLength(1) || nextPosition.y < 0;
}

private string ChangeDirection(string direction) {
    return direction switch {
        "UP" => "RIGHT",
        "RIGHT" => "DOWN",
        "DOWN" => "LEFT",
        "LEFT" => "UP",
        _ => throw new Exception("Unknown direction")
    };
}

private void PrintMap(string[,] map, (int x, int y) position) {
    Console.WriteLine($"Position: ({position.x}, {position.y})");
    for (int i = 0; i < map.GetLength(0); i++) {
        for (int j = 0; j < map.GetLength(1); j++) {
            Console.Write(map[i, j]);
        }
        Console.WriteLine();
    }
}