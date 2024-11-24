namespace AdventOfCode.Day12
{
    class Day12 : DailyTask
    {
        private List<char> _alphabet;

        public override string PartOne()
        {
            GetAlphabet();
            var start = GetStart();
            var end = GetEnd();
            var grid = GetGrid();

            var cameFrom = new Dictionary<Location, Location>();
            var costSoFar = new Dictionary<Location, int>();

            var frontier = new PriorityQueue<Location, int>();

            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.Equals(end))
                {
                    break;
                }

                foreach (var next in grid.Neighbors(current))
                {
                    var newCost = costSoFar[current] + grid.Cost(current, next);
                    if (!costSoFar.TryGetValue(next, out int _) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        int priority = newCost + Heuristic(next, end);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }
            return string.Empty;
        }

        public override string PartTwo()
        {
            throw new NotImplementedException();
        }

        private void GetAlphabet()
        {
            _alphabet = [];

            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                _alphabet.Add(letter);
            }
        }

        private Location GetStart()
        {
            return GetLocation('S');
        }

        private Location GetEnd()
        {
            return GetLocation('E');
        }

        private Location GetLocation(char id)
        {
            Location loc = null!;
            var splittedInput = Input.Split("\n");
            for (int y = 0; y < splittedInput.Length; y++)
            {
                var charArr = splittedInput[y].ToCharArray();
                for (int x = 0; x < charArr.Length; x++)
                {
                    if (charArr[x] == id)
                    {
                        loc = new Location() { X = x, Y = y };
                        break;
                    }
                }
            }
            return loc;
        }

        private Grid GetGrid()
        {
            var splittedInput = Input.Split("\n");
            var grid = new Grid() { Height = splittedInput.Length, Width = splittedInput[0].ToCharArray().Length };
            for (int y = 0; y < splittedInput.Length; y++)
            {
                var charArr = splittedInput[y].ToCharArray();
                for (int x = 0; x < charArr.Length; x++)
                {
                    var location = new Location() { X = x, Y = y, Weight = charArr[x] };
                    if (x == 0 || y == 0 || x == charArr.Length - 1 || y == splittedInput.Length)
                    {
                        grid.walls.Add(location);
                    }
                    else
                    {
                        grid.forests.Add(location);
                    }
                }
            }

            return grid;
        }

        static public int Heuristic(Location a, Location b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}