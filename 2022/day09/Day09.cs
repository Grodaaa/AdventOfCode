using System.Collections;

namespace AdventOfCode.Day09
{
    class Day09 : DailyTask
    {
        List<Coordinates> _tailCoordinates = [];
        Coordinates _head = new();
        Coordinates _tail = new();
        public override string PartOne()
        {
            var steps = GetSteps();

            foreach (var step in steps)
            {
                switch (step.Direction)
                {
                    case Direction.Up:
                        MoveUp(step.Steps);
                        break;
                    case Direction.Down:
                        MoveDown(step.Steps);
                        break;
                    case Direction.Left:
                        MoveLeft(step.Steps);
                        break;
                    case Direction.Right:
                        MoveRight(step.Steps);
                        break;
                    default:
                        break;
                }
            }

            return _tailCoordinates.Count.ToString();
        }

        public override string PartTwo()
        {
            throw new NotImplementedException();
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

        private void MoveUp(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Longitude++;
                if (NeedToMoveTail())
                {
                    _tail.Longitude = _head.Longitude - 1;
                    _tail.Latitude = _head.Latitude;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { Latitude = _tail.Latitude, Longitude = _tail.Longitude });
            }
        }

        private void MoveDown(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Longitude--;
                if (NeedToMoveTail())
                {
                    _tail.Longitude = _head.Longitude + 1;
                    _tail.Latitude = _head.Latitude;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { Latitude = _tail.Latitude, Longitude = _tail.Longitude });
            }
        }

        private void MoveLeft(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Latitude--;
                if (NeedToMoveTail())
                {
                    _tail.Longitude = _head.Longitude;
                    _tail.Latitude = _head.Latitude + 1;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { Latitude = _tail.Latitude, Longitude = _tail.Longitude });
            }
        }

        private void MoveRight(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                _head.Latitude++;
                if (NeedToMoveTail())
                {
                    _tail.Longitude = _head.Longitude;
                    _tail.Latitude = _head.Latitude - 1;
                }

                if (!_tailCoordinates.Any(t => t.Equals(_tail)))
                    _tailCoordinates.Add(new Coordinates() { Latitude = _tail.Latitude, Longitude = _tail.Longitude });
            }
        }

        private bool NeedToMoveTail()
        {
            if (_head.Latitude - _tail.Latitude > 1 || _head.Latitude - _tail.Latitude < -1 ||
               _head.Longitude - _tail.Longitude > 1 || _head.Longitude - _tail.Longitude < -1)
                return true;

            return false;
        }
    }
}