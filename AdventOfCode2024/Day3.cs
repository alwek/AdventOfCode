using System.IO;
using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code - Day 3");

var input = await File.ReadAllTextAsync(@"");

Solve(input, @"mul\(\d{1,3},\d{1,3}\)");
Solve(input, @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");

private void Solve(string input, string pattern) {
    var matches = Regex.Matches(input, pattern);
    var sum = 0;
    var enabled = true;

    foreach (var match in matches) {
        if (enabled && match.ToString().Contains("mul")) {
            var factors = Regex.Matches(match.ToString(), @"[0-9]{1,3}");
            sum += int.Parse(factors[0].Value) * int.Parse(factors[1].Value);
        }
        else if (match.ToString() == "do()") {
            enabled = true;
        }
        else if (match.ToString() == "don't()") {
            enabled = false;
        }
    }

    Console.WriteLine($"Sum of products: {sum}");
}