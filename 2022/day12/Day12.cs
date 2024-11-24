using System.Linq;

namespace AdventOfCode.Day12
{
    class Day12 : DailyTask
    {
        public override string PartOne()
        {
            var alphabet = GetAlphabet();
            var start = GetStart();
            var end = GetEnd();
            var grid = GetGrid(alphabet);

            var cameFrom = new Dictionary<Location, Location>();
            var costSoFar = new Dictionary<Location, int>();

            var frontier = new PriorityQueue<Location, int>();

            frontier.Enqueue(start, 0);

            cameFrom[start] = start;
            costSoFar[start] = 0;
            var steps = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();

                if (current.X == end.X && current.Y == end.Y && current.Weight == end.Weight)
                {
                    break;
                }

                var neighbors = grid.Neighbors(current);
                foreach (var next in neighbors)
                {
                    var newCost = costSoFar[current] + grid.Cost(current, next);
                    if (!costSoFar.TryGetValue(next, out int _) || newCost <= costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        int priority = newCost + Heuristic(next, end);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                        steps++;
                    }
                }
            }
            return string.Empty;
        }

        public override string PartTwo()
        {
            throw new NotImplementedException();
        }

        private Dictionary<char, int> GetAlphabet()
        {
            var alphabet = new Dictionary<char, int>();
            var cost = 1;
            for (char letter = 'a'; letter <= 'z'; letter++)
            {
                alphabet.Add(letter, cost);
                cost++;
            }

            return alphabet;
        }

        private Location GetStart()
        {
            var start = GetLocation('S');
            start.Weight = 1;
            return start;
        }

        private Location GetEnd()
        {
            var end = GetLocation('E');
            end.Weight = 26;
            return end;
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

        private Grid GetGrid(Dictionary<char, int> alphabet)
        {
            var splittedInput = Input.Split("\r\n");
            var grid = new Grid() { Height = splittedInput.Length, Width = splittedInput[0].ToCharArray().Length };
            for (int y = 0; y < splittedInput.Length; y++)
            {
                var charArr = splittedInput[y].ToCharArray();
                for (int x = 0; x < charArr.Length; x++)
                {
                    var weight = alphabet.GetValueOrDefault(charArr[x]);
                    var location = new Location() { X = x, Y = y, Weight = weight };
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