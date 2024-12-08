namespace AdventOfCode.day06
{
    internal class Day06 : DailyTask
    {
        private int _maxX = 0;
        private int _maxY = 0;
        private bool _guardOutsideOfGrid = false;

        public override string PartOne()
        {
            var visitedPositions = new List<Coordinate>();
            var grid = GetGrid();
            _maxX = grid.Max(x => x.X);
            _maxY = grid.Max(x => x.Y);

            var guard = grid.First(x => x.Guard != string.Empty);
            visitedPositions.Add(guard);

            var xDir = 0;
            var yDir = -1;

            while (!_guardOutsideOfGrid)
            {
                var temp = WalkDirection(xDir, yDir, guard, grid);
                visitedPositions.AddRange(temp.Item1);
                guard = temp.Item2;

                if (xDir == 0)
                {
                    if (yDir < 0)
                        xDir = 1;
                    else
                        xDir = -1;

                    yDir = 0;
                }
                else
                {
                    if (xDir < 0)
                        yDir = -1;
                    else
                        yDir = 1;

                    xDir = 0;
                }
            }

            return visitedPositions.Distinct().ToList().Count.ToString();
        }

        public override string PartTwo()
        {
            var visitedPositions = new Dictionary<(int, int), Direction>();
            _guardOutsideOfGrid = false;

            var gridInfo = GetGridPartTwo();
            var grid = gridInfo.Item1;
            _maxX = 130;
            _maxY = 130;

            var guard = gridInfo.Item2;
            visitedPositions.Add(guard, Direction.Up);

            var xDir = 0;
            var yDir = -1;

            while (!_guardOutsideOfGrid)
            {
                guard = WalkDirectionPartTwo(xDir, yDir, guard, grid, visitedPositions).Item1;

                if (xDir == 0)
                {
                    if (yDir < 0)
                        xDir = 1;
                    else
                        xDir = -1;

                    yDir = 0;
                }
                else
                {
                    if (xDir < 0)
                        yDir = -1;
                    else
                        yDir = 1;

                    xDir = 0;
                }
            }

            var successfulObsaclePositions = new List<(int, int)>();

            for (var i = 1; i < visitedPositions.Count; i++)
            {
                _guardOutsideOfGrid = false;
                xDir = 0;
                yDir = -1;
                guard = gridInfo.Item2;

                var testNewPath = new Dictionary<(int, int), Direction>();
                var newGrid = new Dictionary<(int, int), bool>(grid)
                {
                    [visitedPositions.ElementAt(i).Key] = true
                };

                while (!_guardOutsideOfGrid)
                {
                    var result = WalkDirectionPartTwo(xDir, yDir, guard, newGrid, testNewPath, true);
                    if (result.Item2)
                    {
                        successfulObsaclePositions.Add(visitedPositions.ElementAt(i).Key);

                        Console.WriteLine($"Successful obstacles {successfulObsaclePositions.Count}: {visitedPositions.ElementAt(i).Key}");
                        break;
                    }

                    guard = result.Item1;


                    if (xDir == 0)
                    {
                        if (yDir < 0)
                            xDir = 1;
                        else
                            xDir = -1;

                        yDir = 0;
                    }
                    else
                    {
                        if (xDir < 0)
                            yDir = -1;
                        else
                            yDir = 1;

                        xDir = 0;
                    }
                }
            }

            return successfulObsaclePositions.Distinct().Count().ToString();
        }

        public List<Coordinate> GetGrid()
        {
            var grid = new List<Coordinate>();
            var rows = Input.Split(Environment.NewLine);

            for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var row = rows[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    var letter = row[colIndex];
                    grid.Add(new Coordinate() { X = colIndex, Y = rowIndex, Obstacle = letter == '#', Guard = letter == '^' ? letter.ToString() : string.Empty });
                }
            }

            return grid;
        }

        public (Dictionary<(int, int), bool>, (int, int)) GetGridPartTwo()
        {
            var grid = new Dictionary<(int, int), bool>();
            var rows = Input.Split(Environment.NewLine);
            (int, int) guard = (0, 0);

            for (var rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                var row = rows[rowIndex].ToCharArray();
                for (var colIndex = 0; colIndex < row.Length; colIndex++)
                {
                    var letter = row[colIndex];
                    if (letter == '^')
                        guard = (colIndex, rowIndex);
                    grid.Add((colIndex, rowIndex), letter == '#');
                }
            }

            return (grid, guard);
        }
        private (List<Coordinate>, Coordinate) WalkDirection(int xDir, int yDir, Coordinate currentPosition, List<Coordinate> grid)
        {
            var obstacle = false;
            var visitiedPositions = new List<Coordinate>();

            while (obstacle == false)
            {
                var newXPos = currentPosition.X + xDir;
                var newYPos = currentPosition.Y + yDir;
                if (newXPos > _maxX || newXPos < 0 || newYPos > _maxY || newYPos < 0)
                {
                    _guardOutsideOfGrid = true;
                    break;
                }

                var newPos = grid.First(x => x.X == currentPosition.X + xDir && x.Y == currentPosition.Y + yDir);
                if (newPos.Obstacle) { obstacle = true; }
                if (newPos.Obstacle == false)
                {
                    currentPosition = newPos;
                    visitiedPositions.Add(currentPosition);
                }
            }

            return (visitiedPositions, currentPosition);
        }

        private ((int, int), bool) WalkDirectionPartTwo(int xDir, int yDir, (int, int) currentPosition, Dictionary<(int, int), bool> grid, Dictionary<(int, int), Direction> visitedPositions, bool checkLoop = false)
        {
            var obstacle = false;
            var isLoop = false;
            while (obstacle == false)
            {
                var newXPos = currentPosition.Item1 + xDir;
                var newYPos = currentPosition.Item2 + yDir;
                if (newXPos > _maxX || newXPos < 0 || newYPos > _maxY || newYPos < 0)
                {
                    _guardOutsideOfGrid = true;
                    break;
                }

                grid.TryGetValue((currentPosition.Item1 + xDir, currentPosition.Item2 + yDir), out bool isObstacle);
                if (isObstacle) { obstacle = true; }
                else
                {
                    currentPosition = (currentPosition.Item1 + xDir, currentPosition.Item2 + yDir);
                    var couldAdd = visitedPositions.TryAdd(currentPosition, GetDirection(xDir, yDir));
                    if (!couldAdd && checkLoop)
                    {
                        visitedPositions.TryGetValue(currentPosition, out Direction directionAtPos);
                        if (directionAtPos == GetDirection(xDir, yDir))
                        {
                            isLoop = true;
                            break;
                        }
                    }
                }
            }

            return (currentPosition, isLoop);
        }

        private (Coordinate, bool) WalkDirectionSaveDir(int xDir, int yDir, Coordinate currentPosition, List<Coordinate> grid, Dictionary<Coordinate, bool> prevPositions)
        {
            var obstacle = false;
            var isInLoop = false;

            while (obstacle == false && isInLoop == false)
            {
                var newXPos = currentPosition.X + xDir;
                var newYPos = currentPosition.Y + yDir;
                if (newXPos > _maxX || newXPos < 0 || newYPos > _maxY || newYPos < 0)
                {
                    _guardOutsideOfGrid = true;
                    break;
                }

                var newPos = grid.First(x => x.X == currentPosition.X + xDir && x.Y == currentPosition.Y + yDir);

                if (newPos.Obstacle) { obstacle = true; }
                if (newPos.Obstacle == false)
                {
                    currentPosition = newPos;
                    currentPosition.XDir = xDir;
                    currentPosition.YDir = yDir;

                    if (prevPositions.ContainsKey(currentPosition))
                    {
                        isInLoop = true;
                        break;
                    }

                    prevPositions.Add(new Coordinate() { X = currentPosition.X, Y = currentPosition.Y, XDir = currentPosition.XDir, YDir = currentPosition.YDir }, true);
                }
            }

            return (currentPosition, isInLoop);
        }

        private static Direction GetDirection(int xDir, int yDir)
        {
            if (xDir == -1 && yDir == 0)
                return Direction.Left;
            else if (xDir == 1 && yDir == 0)
                return Direction.Right;
            else if (xDir == 0 && yDir == 1)
                return Direction.Down;
            else if (xDir == 0 && yDir == -1)
                return Direction.Up;
            else
                throw new ArgumentException("Direction cannot be mapped!");
        }
    }
}
