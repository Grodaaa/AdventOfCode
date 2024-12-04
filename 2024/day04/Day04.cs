namespace AdventOfCode.day04
{
    internal class Day04 : DailyTask
    {
        private char[] _xmas = ['X', 'M', 'A', 'S'];
        public override string PartOne()
        {
            var numOfXmas = 0;
            var grid = GetGrid();
            var count = 1;

            foreach (var item in grid)
            {
                if (item.Letter == _xmas[0])
                {
                    if (SearchXmas(item, grid, 1, 0, count)) { numOfXmas++; }//Search right
                    if (SearchXmas(item, grid, 1, -1, count)) { numOfXmas++; } //Search right, down
                    if (SearchXmas(item, grid, 0, -1, count)) { numOfXmas++; } //Search down
                    if (SearchXmas(item, grid, -1, -1, count)) { numOfXmas++; } //Search down, left
                    if (SearchXmas(item, grid, -1, 0, count)) { numOfXmas++; } //Search left
                    if (SearchXmas(item, grid, -1, 1, count)) { numOfXmas++; } //Search left, up
                    if (SearchXmas(item, grid, 0, 1, count)) { numOfXmas++; } //Search up
                    if (SearchXmas(item, grid, 1, 1, count)) { numOfXmas++; } //Search up, right
                }
            }

            return numOfXmas.ToString();
        }

        public override string PartTwo()
        {
            var numOfMas = 0;
            var grid = GetGrid();

            foreach (var item in grid)
            {
                if (item.Letter == 'A')
                {
                    if (SearchMas(item, grid)) { numOfMas++; }
                }
            }

            return numOfMas.ToString();
        }

        private bool SearchXmas(Coordinate item, List<Coordinate> grid, int xDir, int yDir, int count)
        {
            var xCoord = item.X + xDir;
            var yCoord = item.Y + yDir;

            var wordFound = true;
            while (count < _xmas.Length)
            {
                var hit = grid.FirstOrDefault(coord => coord.X == xCoord && coord.Y == yCoord && coord.Letter == _xmas[count]);
                if (hit != null)
                {
                    if (xDir < 0) { xCoord--; }
                    else if (xDir > 0) { xCoord++; }

                    if (yDir < 0) { yCoord--; }
                    if (yDir > 0) { yCoord++; }
                }
                else if (hit == null)
                {
                    wordFound = false;
                    break;
                }

                count++;
            }

            return wordFound;
        }

        private static bool SearchMas(Coordinate item, List<Coordinate> grid)
        {
            var mUpperRight = grid.Exists(coord => coord.X == item.X + 1 && coord.Y == item.Y - 1 && coord.Letter == 'M');
            var mLowerRight = grid.Exists(coord => coord.X == item.X + 1 && coord.Y == item.Y + 1 && coord.Letter == 'M');
            var mLowerLeft = grid.Exists(coord => coord.X == item.X - 1 && coord.Y == item.Y + 1 && coord.Letter == 'M');
            var mUpperLeft = grid.Exists(coord => coord.X == item.X - 1 && coord.Y == item.Y - 1 && coord.Letter == 'M');

            var sUpperRight = grid.Exists(coord => coord.X == item.X + 1 && coord.Y == item.Y - 1 && coord.Letter == 'S');
            var sLowerRight = grid.Exists(coord => coord.X == item.X + 1 && coord.Y == item.Y + 1 && coord.Letter == 'S');
            var sLowerLeft = grid.Exists(coord => coord.X == item.X - 1 && coord.Y == item.Y + 1 && coord.Letter == 'S');
            var sUpperLeft = grid.Exists(coord => coord.X == item.X - 1 && coord.Y == item.Y - 1 && coord.Letter == 'S');

            var axis1 = (mUpperRight && sLowerLeft) || (mLowerLeft && sUpperRight);
            var axis2 = (mUpperLeft && sLowerRight) || (mLowerRight && sUpperLeft);

            return axis1 && axis2;
        }

        private List<Coordinate> GetGrid()
        {
            var grid = new List<Coordinate>();
            var rows = Input.Split(Environment.NewLine);

            for (var rowIndex = 0; rowIndex < rows.Count(); rowIndex++)
            {
                var row = rows[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    var letter = row[colIndex];
                    grid.Add(new Coordinate() { X = colIndex, Y = rowIndex, Letter = letter });
                }
            }

            return grid;
        }
    }
}
