namespace AdventOfCode.Day06
{
    class Day06 : DailyTask
    {
        public override string PartOne()
        {
            return GetIndex(4).ToString();
        }

        public override string PartTwo()
        {
            return GetIndex(14).ToString();
        }

        private int GetIndex(int marker)
        {
            var charArray = Input.ToCharArray();

            List<char> startOfPacketMarkerChars = [];
            int index;
            for (index = 0; index < charArray.Length; index++)
            {
                startOfPacketMarkerChars.Add(charArray[index]);
                if (startOfPacketMarkerChars.Count > marker)
                {
                    startOfPacketMarkerChars.RemoveAt(0);
                }

                if (startOfPacketMarkerChars.Count == marker)
                {
                    var distinctList = startOfPacketMarkerChars.Distinct().ToList();
                    if (distinctList.Count == startOfPacketMarkerChars.Count)
                    {
                        break;
                    }
                }
            }

            return index + 1;
        }
    }
}