namespace AdventOfCode.Day12
{
    internal class Location
    {
        public Location() { }
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char Weight { get; set; }
    }

    internal class Grid
    {
        private static readonly Location[] _directions =
        [
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        ];

        public int Width { get; set; }
        public int Height { get; set; }
        public HashSet<Location> walls = [];
        public HashSet<Location> forests = [];

        private bool InBounds(Location id)
        {
            return 0 <= id.X && id.X < Width
                && 0 <= id.Y && id.Y < Height;
        }

        private bool Passable(Location id)
        {
            return !walls.Contains(id);
        }

        public IEnumerable<Location> Neighbors(Location id)
        {
            foreach (var dir in _directions)
            {
                Location next = new(id.X + dir.X, id.Y + dir.Y);
                if (InBounds(next) && Passable(next))
                {
                    yield return next;
                }
            }
        }

        public int Cost(Location a, Location b)
        {
            if(forests.Contains(b) && Math.Abs(b.Weight - a.Weight) == 1)
                return 1;
            return 10000;
        }
    }
}