using System.IO;

Console.WriteLine("Advent of Code - Day 5");

var input = await File.ReadAllLinesAsync(@"");
var rules = input.Where(line => line.Contains("|")).Select(line => line.Split("|")).Select(line => new { x = line[0], y = line[1]});
var updates = input.Where(x => x.Contains(",")).Select(line => line.Split(",").ToList());
var validUpdates = new List<List<string>>();
var invalidUpdates = new List<List<string>>();

foreach (var update in updates) {
    var applicableRules = rules.Where(rule => update.Contains(rule.x) && update.Contains(rule.y));
    var isValid = true;
    
    foreach (var rule in applicableRules) {
        if (update.IndexOf(rule.x) > update.IndexOf(rule.y)){
            isValid = false;
            break;
        }
    }

    if (isValid) {
        validUpdates.Add(update);
    }
    else {
        var rulesToApply = applicableRules.Count();
        do {
            foreach (var rule in applicableRules) {
                if (update.IndexOf(rule.x) > update.IndexOf(rule.y)){
                    update.Remove(rule.x);
                    update.Insert(update.IndexOf(rule.y), rule.x);
                    rulesToApply++;
                }
                else {
                    rulesToApply--;
                }
            }

            if (!invalidUpdates.Contains(update)) {
                invalidUpdates.Add(update);
            }
        } while (rulesToApply > 0);
    }
}

var validSum = validUpdates.Sum(update => int.Parse(update[update.Count/2]));
var invalidSum = invalidUpdates.Sum(update => int.Parse(update[update.Count/2]));
Console.WriteLine($"Sum of valid middle points: {validSum}");
Console.WriteLine($"Sum of invalid middle points: {invalidSum}");