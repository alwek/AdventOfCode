using System.IO;

Console.WriteLine("Advent of Code - Day 1");

var input = await File.ReadAllLinesAsync(@"");
var firstList = new List<int>();
var secondList = new List<int>();
var thirdList = new List<int>();

foreach (var line in input){
    var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    firstList.Add(int.Parse(split[0]));
    secondList.Add(int.Parse(split[1]));
}

firstList.Sort();
secondList.Sort();

for (int i = 0; i < firstList.Count; i++) {
    thirdList.Add(Math.Abs(firstList[i] - secondList[i]));
}

Console.WriteLine($"Total sum: {thirdList.Sum()}");

var similarity = 0;
for (int i = 0; i < firstList.Count; i++){
    var appearances = secondList.Count(secondNumber => secondNumber == firstList[i]);
    similarity += appearances * firstList[i];
}

Console.WriteLine($"Total similarity: {similarity}");