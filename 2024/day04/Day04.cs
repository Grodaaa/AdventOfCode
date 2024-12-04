namespace AdventOfCode.day04
{
    internal class Day04 : DailyTask
    {
        private char[] _xmas = ['X', 'M', 'A', 'S'];
        public override string PartOne()
        {

            var numOfXmas = 0;
            var grid = GetGrid();

            foreach (var item in grid)
            {
                if (item.Letter == _xmas[0])
                {
                    var hitRight = SearchRight(item, grid);
                    if (hitRight) { numOfXmas++; }

                }
            }

            return numOfXmas.ToString();
        }

        public override string PartTwo()
        {
            throw new NotImplementedException();
        }

        private bool SearchRight(Coordinate item, List<Coordinate> grid)
        {
            var xCoord = item.X + 1;
            var xmasPos = 1;

            var wordFound = false;
            while (true)
            {
                var hit = grid.FirstOrDefault(x => x.X == xCoord && x.Y == item.Y && x.Letter == _xmas[xmasPos]);
                if (hit != null && xmasPos < _xmas.Length - 1)
                {
                    xCoord++;
                    xmasPos++;
                }
                else if (hit == null)
                {
                    break;
                }
                else if (hit != null && xmasPos == _xmas.Length - 1)
                {
                    wordFound = true;
                    break;
                }
            }

            return wordFound;
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
