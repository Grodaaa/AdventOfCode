namespace AdventOfCode.Day06
{
    class Day06 : DailyTask
    {
        public override string PartOne()
        {
            var charArray = Input.ToCharArray();

            List<char> startOfPacketMarkerChars = [];
            int index;
            for (index = 0; index < charArray.Length; index++)
            {
                startOfPacketMarkerChars.Add(charArray[index]);
                if (startOfPacketMarkerChars.Count > 4)
                {
                    startOfPacketMarkerChars.RemoveAt(0);
                }

                if (startOfPacketMarkerChars.Count == 4)
                {
                    var distinctList = startOfPacketMarkerChars.Distinct().ToList();
                    if (distinctList.Count == startOfPacketMarkerChars.Count)
                    {
                        break;
                    }
                }
            }

            return (index + 1).ToString();
        }

        public override string PartTwo()
        {
            var charArray = Input.ToCharArray();

            List<char> startOfPacketMarkerChars = [];
            int index;
            for (index = 0; index < charArray.Length; index++)
            {
                startOfPacketMarkerChars.Add(charArray[index]);
                if (startOfPacketMarkerChars.Count > 14)
                {
                    startOfPacketMarkerChars.RemoveAt(0);
                }

                if (startOfPacketMarkerChars.Count == 14)
                {
                    var distinctList = startOfPacketMarkerChars.Distinct().ToList();
                    if (distinctList.Count == startOfPacketMarkerChars.Count)
                    {
                        break;
                    }
                }
            }

            return (index + 1).ToString();
        }
    }
}