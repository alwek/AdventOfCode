using System.IO;
using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code - Day 4");

var input = await File.ReadAllLinesAsync(@"");

SolvePartOne(input);
SolvePartTwo(input);

private void SolvePartOne(string[] input) {
    var lines = new List<string>();
    lines.AddRange(input);
    lines.AddRange(input.Select(x => string.Concat(x.Reverse())));

    for (int i = 0; i < input[0].Length; i++) {
        var column = string.Concat(input.Select(x => x[i]));
        var reversed = string.Concat(column.Reverse());
        lines.Add(column);
        lines.Add(reversed);
    }

    var d1 = input
        .SelectMany((row, rowIndex) => 
            row.Select((column, columnIndex) => new { Key = rowIndex - columnIndex, Value = column }))
        .GroupBy(x => x.Key)
        .OrderBy(x => x.Key)
        .Select(values => 
            string.Concat(values.Select(i => i.Value).ToArray()));
    var d2 = d1.Select(x => string.Concat(x.Reverse()));

    var d3 = input
        .SelectMany((row, rowIndex) => 
            row.Select((column, columnIndex) => new { Key = rowIndex + columnIndex, Value = column }))
        .GroupBy(x => x.Key)
        .OrderBy(x => x.Key)
        .Select(values => 
            string.Concat(values.Select(i => i.Value).ToArray()));
    var d4 = d3.Select(x => string.Concat(x.Reverse()));

    lines.AddRange(d1);
    lines.AddRange(d2);
    lines.AddRange(d3);
    lines.AddRange(d4);

    var sum = lines.Sum(x => Regex.Matches(x, "XMAS").Count);
    Console.WriteLine($"XMAS appears {sum} times");
}

private void SolvePartTwo(string[] input) {
    var array = new string[input[0].Length, input.Count()];

    for (int i = 0; i < input[0].Length; i++) {
        for (int j = 0; j < input.Count(); j++) {
            array[i, j] = input[i][j].ToString();
        }
    }

    var word = "MAS";
    var count = 0;
    for (int i = 1; i < array.GetLength(0) - 1; i++) {
        for (int j = 1; j < array.GetLength(1) - 1; j++) {
            if (array[i, j] != "A") {
                continue;
            }

            var d1 = string.Concat(array[i - 1, j - 1], array[i, j], array[i + 1, j + 1]);
            var d2 = string.Concat(d1.Reverse());
            var d3 = string.Concat(array[i + 1, j - 1], array[i, j], array[i - 1, j + 1]);
            var d4 = string.Concat(d3.Reverse());

            if ((d1 == word || d2 == word) && (d3 == word || d4 == word)) {
                count++;
            }
        }
    }

    Console.WriteLine($"XMAS appears {count} times");
}