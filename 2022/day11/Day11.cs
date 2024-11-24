using System.Text.RegularExpressions;

namespace AdventOfCode.Day11
{
    class Day11 : DailyTask
    {
        public override string PartOne()
        {
            var monkies = GetMonkies();
            var rounds = 0;
            while (rounds < 20)
            {
                for (int i = 0; i < monkies.Count; i++)
                {
                    while (monkies[i].Items.Count > 0)
                    {
                        var item = monkies[i].Items[0];
                        monkies[i].Items.RemoveAt(0);
                        monkies[i].NumberOfInspections++;

                        if (monkies[i].Operation.IsAddition)
                        {
                            if (monkies[i].Operation.NumberIsSelf)
                                item += item;
                            else
                                item += monkies[i].Operation.Number;
                        }
                        else
                        {
                            if (monkies[i].Operation.NumberIsSelf)
                                item *= item;
                            else
                                item *= monkies[i].Operation.Number;
                        }

                        item = (long)Math.Floor((decimal)item / 3);

                        if (item % monkies[i].Test.Divider == 0)
                            monkies[monkies[i].Test.IsTrue].Items.Add(item);
                        else
                            monkies[monkies[i].Test.IsFalse].Items.Add(item);
                    }
                };

                rounds++;
            }

            for (int i = 0; i < monkies.Count; i++)
                Console.WriteLine($"Monkey {i}, num inspections: {monkies[i].NumberOfInspections}");

            var topActiveMonkies = monkies.OrderByDescending(m => m.NumberOfInspections).Select(m => m.NumberOfInspections).Take(2).ToList();
            return (topActiveMonkies[0] * topActiveMonkies[1]).ToString();
        }

        public override string PartTwo()
        {
            var monkies = GetMonkies();
            var rounds = 0;
            var lcm = 1;
            monkies.ForEach(m => {
                lcm *= m.Test.Divider;
            });
            while (rounds < 10000)
            {
                for (int i = 0; i < monkies.Count; i++)
                {
                    while (monkies[i].Items.Count > 0)
                    {
                        var item = monkies[i].Items[0];
                        monkies[i].Items.RemoveAt(0);
                        monkies[i].NumberOfInspections++;

                        if (monkies[i].Operation.IsAddition)
                        {
                            if (monkies[i].Operation.NumberIsSelf)
                                item += item;
                            else
                                item += monkies[i].Operation.Number;
                        }
                        else
                        {
                            if (monkies[i].Operation.NumberIsSelf)
                                item *= item;
                            else
                                item *= monkies[i].Operation.Number;
                        }

                        item %= lcm;

                        if (item % monkies[i].Test.Divider == 0) {
                            monkies[monkies[i].Test.IsTrue].Items.Add(item);
                        }
                        else
                            monkies[monkies[i].Test.IsFalse].Items.Add(item);
                    }
                };

                rounds++;
            }

            for (int i = 0; i < monkies.Count; i++)
                Console.WriteLine($"Monkey {i}, num inspections: {monkies[i].NumberOfInspections}");

            var topActiveMonkies = monkies.OrderByDescending(m => m.NumberOfInspections).Select(m => m.NumberOfInspections).Take(2).ToList();
            return (topActiveMonkies[0] * topActiveMonkies[1]).ToString();
        }

        private List<Monkey> GetMonkies()
        {
            var monkies = new List<Monkey>();
            var monkiesInput = Input.Split("\r\n\r\n");
            foreach (var monkeyInput in monkiesInput)
            {
                var arr = monkeyInput.Split("\r\n");
                if (arr.Length == 6)
                {
                    monkies.Add(
                        new Monkey()
                        {
                            Items = GetItems(arr[1]),
                            Operation = GetOperation(arr[2]),
                            Test = GetTest(arr[3..6])
                        }
                    );
                }
            }

            return monkies;
        }

        private static List<long> GetItems(string itemsString)
        {
            var matches = Regex.Matches(itemsString, @"\d+");
            return matches.Cast<Match>().Select(m => long.Parse(m.Value)).ToList();
        }

        private static Operation GetOperation(string operationString)
        {
            var splittedString = operationString.Split(":");
            var formulaParts = splittedString[1].TrimStart().Split(" ");

            var operation = new Operation()
            {
                IsAddition = formulaParts[3] == "+"
            };
            if (formulaParts[4] == "old")
                operation.NumberIsSelf = true;
            else
            {
                operation.NumberIsSelf = false;
                operation.Number = long.Parse(formulaParts[4]);
            }

            return operation;
        }

        private static Test GetTest(string[] testInput)
        {
            var test = new Test();
            List<int> numbers = [];

            // Regex pattern to match numbers
            var pattern = @"\d+";

            foreach (var line in testInput)
            {
                var matches = Regex.Matches(line, pattern);
                numbers.AddRange(matches.Cast<Match>().Select(m => int.Parse(m.Value)));
            }

            test.Divider = numbers[0];
            test.IsTrue = numbers[1];
            test.IsFalse = numbers[2];

            return test;
        }

    }
}