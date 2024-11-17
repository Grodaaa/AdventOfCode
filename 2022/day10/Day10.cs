using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Day10
{
    class Day10 : DailyTask
    {
        int _cycles = 0;
        int _cyclePixelPos = 0;
        int _x = 1;
        List<int> _signalStrengths = [];
        int _nextCycleToCheck = 20;
        List<int> _spritePos = [];
        readonly string[] _crtRows = new string[6];
        string _currentCrtRow = string.Empty;
        int _currentCrtRowCount = 0;
        readonly List<int> _rowEnds = [39, 79, 119, 159, 199, 239];
        public override string PartOne()
        {
            var instructions = GetInstructions();

            instructions.ForEach(instructionString =>
            {
                var instruction = instructionString.Split(" ");
                if (instruction[0] == "addx")
                {
                    HandleAddXPartOne(int.Parse(instruction[1]));
                }
                else if (instruction[0] == "noop")
                {
                    HandleNoOpPartOne();
                }
            });

            return _signalStrengths.Sum().ToString();
        }

        public override string PartTwo()
        {
            _x = 1;
            _cycles = 0;
            var instructions = GetInstructions();

            var numPixels = 240;
            _spritePos = [0, 1, 2];
            while (_cycles < numPixels)
            {
                instructions.ForEach(instructionString =>
                {
                    var instruction = instructionString.Split(" ");
                    if (instruction[0] == "addx")
                    {
                        HandleAddXPartTwo(int.Parse(instruction[1]));
                    }
                    else if (instruction[0] == "noop")
                    {
                        HandleNoOpPartTwo();
                    }
                });
            }

            foreach (var crtRow in _crtRows)
            {
                Console.WriteLine(crtRow);
            }

            return string.Empty;
        }

        private List<string> GetInstructions()
        {
            return [.. Input.Split("\n")];
        }

        #region PartOne
        private void HandleAddXPartOne(int value, int counter = 0)
        {
            _cycles++;
            CheckCyclePartOne();
            if (counter == 0)
            {
                HandleAddXPartOne(value, 1);
            }
            else
            {
                _x += value;
            }
        }

        private void HandleNoOpPartOne()
        {
            _cycles++;
            CheckCyclePartOne();
        }

        private void CheckCyclePartOne()
        {
            if (_cycles == _nextCycleToCheck)
            {
                _signalStrengths.Add(_cycles * _x);
                _nextCycleToCheck += 40;
            }
        }
        #endregion
        #region  PartTwo
        private void HandleAddXPartTwo(int value, int counter = 0)
        {
            IncrementCycleAndPrint();
            if (counter == 0)
            {
                HandleAddXPartTwo(value, 1);
            }
            else
            {
                _x += value;
                _spritePos = [_x - 1, _x, _x + 1];
            }
        }

        private void HandleNoOpPartTwo()
        {
            IncrementCycleAndPrint();
        }
        private void IncrementCycleAndPrint()
        {
            if (_spritePos.Any(x => x == _cyclePixelPos))
            {
                _currentCrtRow += '#';
            }
            else
            {
                _currentCrtRow += '.';
            }

            if (_rowEnds.Any(r => r == _cycles))
            {
                _crtRows[_currentCrtRowCount] = _currentCrtRow;
                _currentCrtRow = "";
                _currentCrtRowCount++;
            }
            _cycles++;
            if (_rowEnds.Any(r => r == _cycles - 1))
                _cyclePixelPos = 0;
            else
                _cyclePixelPos++;
        }
        #endregion

    }
}