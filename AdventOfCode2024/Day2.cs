using System.IO;

Console.WriteLine("Advent of Code - Day 2");

var input = await File.ReadAllLinesAsync(@"");
var reports = new List<int[]>();

foreach (var line in input) {
    var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray();
    reports.Add(split);
}

var firstRun = Solve(reports, false);
var secondRun = Solve(firstRun.runAgain, true);

Console.WriteLine($"Safe levels: {firstRun.safe}");
Console.WriteLine(($"Tolerated levels: {secondRun.safe + firstRun.safe}"));

private (int safe, List<int[]> runAgain) Solve(List<int[]> reports, bool isPartTwo) {
    var unsafeReports = new List<int[]>();
    var safe = 0;

    foreach (var report in reports) {
        var isSafe = true;
        var direction = Math.Sign(report[0] - report[^1]);

        for (int i = 0; i < report.Length - 1; i++) { 
            var difference = report[i] - report[i + 1];
            var sign = Math.Sign(difference);
            var abs = Math.Abs(difference);

            if (!IsValid(direction, sign, abs)) {
                unsafeReports.Add(RemoveFaultyLevel(report, i));
                isSafe = false;
                break;
            }
        }

        if (isSafe) {
            safe++;
        }
    }

    return (safe, unsafeReports);
}

private bool IsValid(int reportDirection, int levelDirection, int difference) {
    return reportDirection == levelDirection && difference >= 1 && difference <= 3;
}

private int[] RemoveFaultyLevel(int[] report, int index) {
    var unsafeReport = report.ToList();
    unsafeReport.RemoveAt(index);
    return unsafeReport.ToArray();
}

private void PrintInput(List<int[]> reports) {
    foreach (var report in reports) {
        for (int i = 0; i < report.Length; i++) {
            Console.Write($"{report[i]} ");
        }
        Console.WriteLine();
    }
}