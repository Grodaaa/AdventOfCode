using System.Collections;

namespace AdventOfCode.Day09
{
    class Day09 : DailyTask
    {
        List<Coordinates> _tailCoordinates = [];
        List<Coordinates> _rope = [];
        readonly Coordinates _head = new();
        readonly Coordinates _tail = new();

        public override string PartOne()
        {
            var steps = GetSteps();
            _tailCoordinates = [];

            foreach (var step in steps)
            {
                switch (step.Direction)
                {
                    case Direction.Up:
                        MoveShortRopeUp(step.Steps);
                        break;
                    case Direction.Down:
                        MoveShortRopeDown(step.Steps);
                        break;
                    case Direction.Left:
                        MoveShortRopeLeft(step.Steps);
                        break;
                    case Direction.Right:
                        MoveShortRopeRight(step.Steps);
                        break;
                    default:
                        break;
                }
            }

            return _tailCoordinates.Count.ToString();
        }

        public override string PartTwo()
        {
            var steps = GetSteps();
            _tailCoordinates = [new Coordinates()];
            _rope = [];
            for (int i = 0; i < 10; i++)
            {
                _rope.Add(new Coordinates());
            }

            foreach (var step in steps)
            {
                for (int i = 0; i < step.Steps; i++)
                {
                    for (int ropeIndex = 0; ropeIndex < _rope.Count; ropeIndex++)
                    {
                        if (ropeIndex == 0)
                        {
                            switch (step.Direction)
                            {
                                case Direction.Up:
                                    _rope[ropeIndex].Y++;
                                    break;
                                case Direction.Down:
                                    _rope[ropeIndex].Y--;
                                    break;
                                case Direction.Left:
                                    _rope[ropeIndex].X--;
                                    break;
                                case Direction.Right:
                                    _rope[ropeIndex].X++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            var currentKnot = _rope[ropeIndex];
                            var prevKnot = _rope[ropeIndex - 1];

                            if (Math.Abs(prevKnot.X - currentKnot.X) > 1 || Math.Abs(prevKnot.Y - currentKnot.Y) > 1)
                            {
                                if (NeedToMoveDownAndRight(prevKnot, currentKnot))
                                {
                                    currentKnot.X++;
                                    currentKnot.Y--;
                                }
                                else if (NeedToMoveDownAndLeft(prevKnot, currentKnot))
                                {
                                    currentKnot.X--;
                                    currentKnot.Y--;
                                }
                                else if (NeedToMoveUpAndLeft(prevKnot, currentKnot))
                                {
                                    currentKnot.X--;
                                    currentKnot.Y++;
                                }
                                else if (NeedToMoveUpAndRight(prevKnot, currentKnot))
                                {
                                    currentKnot.X++;
                                    currentKnot.Y++;
                                }
                                else if (NeedToMoveHorizontalRight(prevKnot, currentKnot))
                                {
                                    currentKnot.X++;
                                }
                                else if (NeedToMoveVerticalDown(prevKnot, currentKnot))
                                {
                                    currentKnot.Y--;
                                }
                                else if (NeedToMoveHorizontalLeft(prevKnot, currentKnot))
                                {
                                    currentKnot.X--;
                                }
                                else if (NeedToMoveVerticalUp(prevKnot, currentKnot))
                                {
                                    currentKnot.Y++;
                                }

                                _rope[ropeIndex] = currentKnot;
                                if (ropeIndex == _rope.Count - 1 && !_tailCoordinates.Any(t => t.Equals(currentKnot)))
                                {
                                    _tailCoordinates.Add(new Coordinates() { X = currentKnot.X, Y = currentKnot.Y });
                                }
                            }
                        }
                    }
                }
            }

            return _tailCoordinates.Count.ToString();
        }

        #region InitSteps
        private List<Step> GetSteps()
        {
            var steps = new List<Step>();
            var splittedInput = Input.Split("\n");
            foreach (var input in splittedInput)
            {
                var stepString = input.Split(" ");
                steps.Add(new Step() { Direction = GetDirection(stepString[0]), Steps = int.Parse(stepString[1]) });
            }

            return steps;
        }

        private static Direction GetDirection(string directionString)
        {
            return directionString switch
            {
                "U" => Direction.Up,
                "D" => Direction.Down,
                "L" => Direction.Left,
                "R" => Direction.Right,
                _ => throw new ArgumentException($"{directionString} cannot be parsed to enum Direction"),
            };
        }
        #endregion

        #region PartOne
        private void MoveShortRopeUp(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Y++;
                if (NeedToMoveTail())
                {
                    _tail.Y = _head.Y - 1;
                    _tail.X = _head.X;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { X = _tail.X, Y = _tail.Y });
            }
        }

        private void MoveShortRopeDown(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Y--;
                if (NeedToMoveTail())
                {
                    _tail.Y = _head.Y + 1;
                    _tail.X = _head.X;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { X = _tail.X, Y = _tail.Y });
            }
        }

        private void MoveShortRopeLeft(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.X--;
                if (NeedToMoveTail())
                {
                    _tail.Y = _head.Y;
                    _tail.X = _head.X + 1;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { X = _tail.X, Y = _tail.Y });
            }
        }

        private void MoveShortRopeRight(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.X++;
                if (NeedToMoveTail())
                {
                    _tail.Y = _head.Y;
                    _tail.X = _head.X - 1;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { X = _tail.X, Y = _tail.Y });
            }
        }

        private bool NeedToMoveTail()
        {
            if (_head.X - _tail.X > 1 || _head.X - _tail.X < -1 ||
               _head.Y - _tail.Y > 1 || _head.Y - _tail.Y < -1)
                return true;

            return false;
        }
        #endregion
        private static bool NeedToMoveHorizontalLeft(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y == currentKnot.Y && prevKnot.X < currentKnot.X;
        }

        private static bool NeedToMoveHorizontalRight(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y == currentKnot.Y && prevKnot.X > currentKnot.X;
        }

        private static bool NeedToMoveVerticalUp(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y > currentKnot.Y && prevKnot.X == currentKnot.X;
        }

        private static bool NeedToMoveVerticalDown(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y < currentKnot.Y && prevKnot.X == currentKnot.X;
        }

        private static bool NeedToMoveUpAndRight(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y > currentKnot.Y && prevKnot.X > currentKnot.X;
        }

        private static bool NeedToMoveUpAndLeft(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y > currentKnot.Y && prevKnot.X < currentKnot.X;
        }

        private static bool NeedToMoveDownAndRight(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y < currentKnot.Y && prevKnot.X > currentKnot.X;
        }

        private static bool NeedToMoveDownAndLeft(Coordinates prevKnot, Coordinates currentKnot)
        {
            return prevKnot.Y < currentKnot.Y && prevKnot.X < currentKnot.X;
        }
    }
}