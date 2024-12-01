using System.Reflection;
using AdventOfCode;
if (args.Length != 1)
    throw new ArgumentException("Invalid number of arguments.");

var day = int.Parse(args[0]).ToString().PadLeft(2, '0');
var taskName = $"Day{day}";
Console.WriteLine($"Args {taskName}");
try
{
    var task = FindTask(taskName);

    var partOne = task.PartOne();
    Console.WriteLine($"Part One: {partOne}");

    var partTwo = task.PartTwo();
    Console.WriteLine($"Part Two: {partTwo}");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Environment.Exit(-1);
}

static DailyTask FindTask(string name)
{
    var runnable = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.BaseType == typeof(DailyTask))
                    .FirstOrDefault(task => task.Name == name) ?? throw new ArgumentException($"Cannot find DailyTask for day{name}");

    return (DailyTask)Activator.CreateInstance(runnable)!;
}
