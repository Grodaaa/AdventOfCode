using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Day05
{
    class Day05 : DailyTask
    {
        public override string PartOne()
        {
            var stackAndSteps = Input.Split("\n\n");
            var stacks = GetStacks(stackAndSteps[0]);
            var steps = GetSteps(stackAndSteps[1]);

            //Move crates 
            foreach (var step in steps)
            {
                var cratesToMove = new List<string>();
                stacks.ForEach(stack =>
                {
                    if (stack.Id == step.From)
                    {
                        cratesToMove = stack.Crates.TakeLast(step.Move).ToList();
                        cratesToMove.Reverse();
                        stack.Crates.RemoveRange(stack.Crates.Count - step.Move, step.Move);
                    }
                });

                stacks.ForEach(stack =>
                {
                    if (stack.Id == step.To)
                    {
                        stack.Crates.AddRange(cratesToMove);
                    }
                });
            }

            //Get top crates
            var topCrates = "";
            stacks.ForEach(s =>
            {
                topCrates += s.Crates.LastOrDefault();
            });

            return topCrates;
        }

        public override string PartTwo()
        {
            var stackAndSteps = Input.Split("\n\n");
            var stacks = GetStacks(stackAndSteps[0]);
            var steps = GetSteps(stackAndSteps[1]);

            //Move crates 
            foreach (var step in steps)
            {
                var cratesToMove = new List<string>();
                stacks.ForEach(stack =>
                {
                    if (stack.Id == step.From)
                    {
                        cratesToMove = stack.Crates.TakeLast(step.Move).ToList();
                        stack.Crates.RemoveRange(stack.Crates.Count - step.Move, step.Move);
                    }
                });

                stacks.ForEach(stack =>
                {
                    if (stack.Id == step.To)
                    {
                        stack.Crates.AddRange(cratesToMove);
                    }
                });
            }

            //Get top crates
            var topCrates = "";
            stacks.ForEach(s =>
            {
                topCrates += s.Crates.LastOrDefault();
            });

            return topCrates;
        }

        private static List<Stack> GetStacks(string stackInput)
        {
            var stacks = new List<Stack>();
            var rows = stackInput.Split("\n");

            var lastRow = rows[^1];
            var rowLength = lastRow.Length;
            var stackArray = lastRow.ToCharArray();
            for (var i = 0; i < stackArray.Length; i++)
            {
                if (int.TryParse(stackArray[i].ToString(), out int stackId))
                {
                    var stack = new Stack() { Id = stackId, Column = i };
                    stacks.Add(stack);
                }
            }

            var crateRows = rows[0..^1];
            var crateIndexes = stacks.Select(x => x.Column).ToList();
            foreach (var crateRow in crateRows)
            {
                var arr = crateRow.ToCharArray();
                for (var i = 0; i < crateIndexes.Count; i++)
                {
                    var crateIndex = crateIndexes[i];
                    var character = arr[crateIndex];
                    if (char.IsLetter(character))
                    {
                        stacks.ForEach(s =>
                        {
                            if (s.Column == crateIndex)
                                s.Crates.Insert(0, character.ToString());
                        });
                    }
                }
            }

            return stacks;
        }

        private static List<Step> GetSteps(string stepInput)
        {
            var rows = stepInput.Split("\n");
            var steps = new List<Step>();

            foreach (var row in rows)
            {
                List<int> digits =
                [
                    // Add the matched number (as an integer) to the list
                    .. from Match match in Regex.Matches(row, @"\d+")
                                    select int.Parse(match.Value),
                ];
                var step = new Step() { Move = digits[0], From = digits[1], To = digits[2] };
                steps.Add(step);
            }

            return steps;
        }
    }
}