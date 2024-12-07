namespace AdventOfCode.day05
{
    internal class Day05 : DailyTask
    {
        public override string PartOne()
        {
            var correctInstructions = new List<List<int>>();

            var inputs = Input.Split([Environment.NewLine + Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
            var rules = GetOrderingRules(inputs[0]);
            var instructions = GetInstructions(inputs[1]);

            foreach (var instruction in instructions)
            {
                var incorrectInstruction = false;
                for (int i = 0; i < instruction.Count; i++)
                {
                    var page = instruction[i];
                    var cantBefore = rules.Where(x => x.OrderOne == page).Select(x => x.OrderTwo);
                    var cantBeAfter = rules.Where(x => x.OrderTwo == page).Select(x => x.OrderOne);
                    if (i != 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (cantBefore.Any(x => x == instruction[j]))
                            {
                                incorrectInstruction = true;
                                break;
                            }
                        }
                        if (incorrectInstruction) break;
                    }

                    if (i != instruction.Count - 1)
                    {
                        for (int j = i + 1; j < instruction.Count; j++)
                        {
                            if (cantBeAfter.Any(x => x == instruction[j]))
                            {
                                incorrectInstruction = true;
                                break;
                            }
                        }
                        if (incorrectInstruction) break;
                    }
                };

                if (!incorrectInstruction) correctInstructions.Add(instruction);

            }

            var sum = 0;
            foreach (var instruction in correctInstructions)
            {
                sum += instruction[instruction.Count / 2];
            }

            return sum.ToString();
        }

        public override string PartTwo()
        {
            var incorrectInstructions = new List<List<int>>();
            var correctedInstructions = new List<List<int>>();

            var inputs = Input.Split([Environment.NewLine + Environment.NewLine], StringSplitOptions.RemoveEmptyEntries);
            var rules = GetOrderingRules(inputs[0]);
            var instructions = GetInstructions(inputs[1]);

            foreach (var instruction in instructions)
            {
                var incorrectInstruction = false;
                for (int i = 0; i < instruction.Count; i++)
                {
                    var page = instruction[i];
                    var cantBefore = rules.Where(x => x.OrderOne == page).Select(x => x.OrderTwo);
                    var cantBeAfter = rules.Where(x => x.OrderTwo == page).Select(x => x.OrderOne);
                    if (i != 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (cantBefore.Any(x => x == instruction[j]))
                            {
                                incorrectInstruction = true;
                                break;
                            }
                        }
                        if (incorrectInstruction) break;
                    }

                    if (i != instruction.Count - 1)
                    {
                        for (int j = i + 1; j < instruction.Count; j++)
                        {
                            if (cantBeAfter.Any(x => x == instruction[j]))
                            {
                                incorrectInstruction = true;
                                break;
                            }
                        }
                        if (incorrectInstruction) break;
                    }
                };

                if (incorrectInstruction) incorrectInstructions.Add(instruction);
            }

            foreach (var incorrectInstruction in incorrectInstructions)
            {
                var correctInstruction = new List<int>();

                for (int i = 0; i < incorrectInstruction.Count; i++)
                {
                    var page = incorrectInstruction[i];
                    var canBeBefore = rules.Where(x => x.OrderTwo == page).Select(x => x.OrderOne);
                    var canBeAfter = rules.Where(x => x.OrderOne == page).Select(x => x.OrderTwo).ToList();

                    if (correctInstruction.Count == 0) correctInstruction.Add(page);
                    else
                    {
                        var added = false;
                        for (int j = 0; j < correctInstruction.Count; j++)
                        {
                            if (canBeAfter.Contains(correctInstruction[j]))
                            {
                                correctInstruction.Insert(j, page);
                                added = true;
                                break;
                            }
                        }

                        if (!added) correctInstruction.Add(page);
                    }
                };

                correctedInstructions.Add(correctInstruction);
            }

            var sum = 0;
            foreach (var instruction in correctedInstructions)
            {
                sum += instruction[instruction.Count / 2];
            }

            return sum.ToString();
        }

        private static List<OrderingRule> GetOrderingRules(string input)
        {
            var orderingRules = new List<OrderingRule>();
            var rules = input.Split(Environment.NewLine);
            foreach (var rule in rules)
            {
                var splittedRule = rule.Split("|");
                orderingRules.Add(new OrderingRule() { OrderOne = int.Parse(splittedRule[0]), OrderTwo = int.Parse(splittedRule[1]) });
            }

            return orderingRules;
        }

        private static List<List<int>> GetInstructions(string input)
        {
            var instructions = new List<List<int>>();
            foreach (var item in input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var instruction = new List<int>();
                foreach (var page in item.Split(",")) { instruction.Add(int.Parse(page)); }
                instructions.Add(instruction);
            }

            return instructions;
        }
    }
}
