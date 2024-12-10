namespace AdventOfCode.day07
{
    internal class Day07 : DailyTask
    {
        public override string PartOne()
        {
            var operations = GetOperations();
            var validOperations = new List<(long, List<long>)>();

            foreach (var item in operations)
            {
                var expectedRes = item.Item1;

                var numOperationsInEq = item.Item2.Count - 1;

                var operatorVariations = GetOperators([], numOperationsInEq);
                foreach (var operators in operatorVariations)
                {
                    var testSum = item.Item2[0];
                    var countOp = 0;
                    for (var countItem = 1; countItem < item.Item2.Count; countItem++)
                    {
                        var temp = item.Item2[countItem];
                        var op = operators[countOp];
                        switch (op)
                        {
                            case Operators.Add:
                                testSum += temp;
                                break;
                            case Operators.Multiply:
                                testSum *= temp;
                                break;
                            default:
                                break;
                        }
                        countOp++;
                    }

                    if (testSum == expectedRes)
                    {
                        validOperations.Add(item);
                    }
                }
            }

            return validOperations.Distinct().Sum(x => x.Item1).ToString();
        }

        public override string PartTwo()
        {
            var operations = GetOperations();
            var validOperations = new List<(long, List<long>)>();

            foreach (var item in operations)
            {
                var expectedRes = item.Item1;

                var numbers = new List<long>(item.Item2);
                var numOperationsInEq = numbers.Count - 1;

                var operatorVariations = GetOperatorsPartTwo([], numOperationsInEq);
                foreach (var operators in operatorVariations)
                {
                    var testSum = numbers[0];
                    var countOp = 0;
                    for (var countItem = 1; countItem < numbers.Count; countItem++)
                    {
                        var temp = numbers[countItem];
                        var op = operators[countOp];
                        switch (op)
                        {
                            case Operators.Add:
                                testSum += temp;
                                break;
                            case Operators.Multiply:
                                testSum *= temp;
                                break;
                            case Operators.Concatination:
                                testSum = Convert.ToInt64(string.Format("{0}{1}", testSum, numbers[countItem]));
                                break;
                            default:
                                break;
                        }
                        countOp++;
                    }

                    if (testSum == expectedRes)
                    {
                        validOperations.Add(item);
                    }

                    numbers = new List<long>(item.Item2);
                }
            }

            return validOperations.Distinct().Sum(x => x.Item1).ToString();
        }

        private List<(long, List<long>)> GetOperations()
        {
            var operations = new List<(long, List<long>)>();
            foreach (var item in Input.Split(Environment.NewLine))
            {
                var items = item.Split(":");
                var result = long.Parse(items[0]);

                var numbers = new List<long>();

                foreach (var item1 in items[1].Split(" "))
                {
                    if (long.TryParse(item1, out long res))
                        numbers.Add(res);

                }
                operations.Add((result, numbers));
            }

            return operations;
        }

        private static List<List<Operators>> GetOperators(List<List<Operators>> operators, int numOperationsInEq)
        {
            var newList = new List<List<Operators>>();
            if (operators.Count == 0)
            {
                newList.Add([Operators.Add]);
                newList.Add([Operators.Multiply]);
            }
            else
            {
                foreach (var op in operators)
                {
                    var add = new List<Operators>(op)
                {
                    Operators.Add
                };
                    var mul = new List<Operators>(op)
                {
                    Operators.Multiply
                };

                    newList.Add(add);
                    newList.Add(mul);
                }
            }

            if (newList.First().Count < numOperationsInEq)
                newList = GetOperators(newList, numOperationsInEq);

            return newList;
        }

        private static List<List<Operators>> GetOperatorsPartTwo(List<List<Operators>> operators, int numOperationsInEq)
        {
            var newList = new List<List<Operators>>();
            if (operators.Count == 0)
            {
                newList.Add([Operators.Add]);
                newList.Add([Operators.Multiply]);
                newList.Add([Operators.Concatination]);
            }
            else
            {
                foreach (var op in operators)
                {
                    var add = new List<Operators>(op)
                    {
                        Operators.Add
                    };
                    var mul = new List<Operators>(op)
                    {
                        Operators.Multiply
                    };
                    var con = new List<Operators>(op)
                    {
                        Operators.Concatination
                    };

                    newList.Add(add);
                    newList.Add(mul);
                    newList.Add(con);
                }
            }

            if (newList.First().Count < numOperationsInEq)
                newList = GetOperatorsPartTwo(newList, numOperationsInEq);

            return newList;
        }
    }

    internal enum Operators
    {
        Add,
        Multiply,
        Concatination
    }
}
