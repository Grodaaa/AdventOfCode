using static System.Net.Mime.MediaTypeNames;

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
        public int Weight { get; set; }
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
                    yield return GetNeighborLocation(next);
                }
            }
        }

        private Location GetNeighborLocation(Location id)
        {
            var allItems = new List<Location>(forests);
            allItems.AddRange(walls);
            foreach (var item in allItems)
            {
                if (item.X == id.X && item.Y == id.Y)
                    return item;
            }

            throw new ArgumentException($"Value with x coord {id.X} and y coord {id.Y} dosen't exist in grid");
        }

        public int Cost(Location a, Location b)
        {
            if (forests.Contains(b) || walls.Contains(b))
                return Math.Abs(b.Weight - a.Weight);

            throw new ArgumentException($"Location {b.Weight} dosen't exist.");
        }
    }
}