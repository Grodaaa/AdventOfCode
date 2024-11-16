using System.Runtime.CompilerServices;

namespace AdventOfCode.Day08
{
    class Day08 : DailyTask
    {
        readonly int minLatitude = 0;
        int maxLatitude = 0;
        readonly int minLongitude = 0;
        int maxLongitude = 0;

        public override string PartOne()
        {
            List<Tree> trees = [];
            var visibleTrees = 0;

            var treeGrid = Input.Split("\n");
            maxLongitude = treeGrid.Length - 1;

            for (int latitude = 0; latitude < treeGrid.Length; latitude++)
            {
                var treeRow = treeGrid[latitude].ToCharArray();
                maxLatitude = treeRow.Length - 1;
                for (int longitude = 0; longitude < treeRow.Length; longitude++)
                {
                    if (IsEdgeTree(longitude, latitude))
                    {
                        visibleTrees++;
                    }
                    else
                    {
                        var tree = char.GetNumericValue(treeRow[longitude]);
                        if (IsVisibleFromTop(latitude, longitude, tree, treeGrid) ||
                            IsVisibleFromBottom(latitude, longitude, tree, treeGrid) ||
                            IsVisibleFromLeft(longitude, tree, treeRow) ||
                            IsVisibleFromRight(longitude, tree, treeRow))
                        {
                            visibleTrees++;
                        }

                    }
                    trees.Add(new Tree() { Longitude = latitude, Latitude = longitude, Height = char.GetNumericValue(treeRow[longitude]) });
                }
            }

            return visibleTrees.ToString();
        }

        public override string PartTwo()
        {
            throw new NotImplementedException();
        }

        private bool IsEdgeTree(int latitude, int longitude)
        {
            return latitude == minLatitude ||
                   latitude == maxLatitude ||
                   longitude == minLongitude ||
                   longitude == maxLongitude;
        }

        private static bool IsVisibleFromTop(int latitude, int longitude, double treeHeight, string[] treeGrid)
        {
            var treesOnTop = new List<char>();
            for (var i = 0; i < latitude; i++)
            {
                var treeRow = treeGrid[i].ToCharArray();
                treesOnTop.Add(treeRow[longitude]);
            }
            return IsVisible(treeHeight, treesOnTop);
        }
        private static bool IsVisibleFromBottom(int latitude, int longitude, double treeHeight, string[] treeGrid)
        {
            var treesBelow = new List<char>();
            for (var i = latitude + 1; i < treeGrid.Length; i++)
            {
                var treeRow = treeGrid[i].ToCharArray();
                treesBelow.Add(treeRow[longitude]);
            }
            return IsVisible(treeHeight, treesBelow);
        }
        private static bool IsVisibleFromLeft(int longitude, double treeHeight, char[] treeRow)
        {
            var treesToTheLeft = treeRow.ToList().GetRange(0, longitude);
            treesToTheLeft.Reverse();
            return IsVisible(treeHeight, treesToTheLeft);
        }
        private static bool IsVisibleFromRight(int longitude, double treeHeight, char[] treeRow)
        {
            var treesToTheRight = treeRow.ToList().GetRange(longitude + 1, treeRow.Length - longitude - 1);
            return IsVisible(treeHeight, treesToTheRight);
        }

        private static bool IsVisible(double treeHeight, List<char>? neighboors)
        {
            var isVisible = false;
            foreach (var tree in neighboors ?? [])
            {
                if (char.GetNumericValue(tree) < treeHeight)
                    isVisible = true;
                else
                {
                    isVisible = false;
                    break;
                }
            }

            return isVisible;
        }
    }
}