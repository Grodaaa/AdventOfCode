namespace AdventOfCode.day02
{
    internal class Day02 : DailyTask
    {
        public override string PartOne()
        {
            var reports = GetReports();
            var safeReports = 0;

            foreach (var report in reports)
            {
                var descendingList = report.OrderByDescending(x => x);
                var ascendingList = report.OrderBy(x => x);

                if (report.SequenceEqual(descendingList) || report.SequenceEqual(ascendingList))
                {
                    var isSafe = false;
                    for (var i = 0; i < report.Count - 1; i++)
                    {
                        var diff = Math.Abs(report[i] - report[i + 1]);
                        if (diff >= 1 && diff <= 3)
                            isSafe = true;
                        else
                        {
                            isSafe = false;
                            break;
                        }
                    }
                    if (isSafe) { safeReports++; }
                }
            }

            return safeReports.ToString();
        }

        public override string PartTwo()
        {
            var reports = GetReports();
            var safeReports = 0;

            foreach (var report in reports)
            {
                var isSafe = IsSafeReport(report);
                if (!isSafe) 
                {
                    var count = 0;
                    var startLenght = report.Count;
                    while (count < report.Count)
                    {
                        var tempReport = new List<int>(report);
                        tempReport.RemoveAt(count);
                        isSafe = IsSafeReport(tempReport);
                        if (isSafe)
                        {
                            break;
                        }
                        else
                        {
                            count++;
                        }
                    }
                }

                if(isSafe) { safeReports++; }
            }

            return safeReports.ToString();
        }

        private List<List<int>> GetReports()
        {
            var reports = new List<List<int>>();
            var lines = Input.Split(Environment.NewLine);
            foreach (var line in lines)
            {
                var items = line.Split(" ");
                var report = new List<int>();
                foreach (var item in items)
                {
                    report.Add(int.Parse(item));
                }
                reports.Add(report);
            }
            return reports;
        }

        private bool IsSafeReport(List<int> report)
        {
            var descendingList = report.OrderByDescending(x => x);
            var ascendingList = report.OrderBy(x => x);

            if (report.SequenceEqual(descendingList) || report.SequenceEqual(ascendingList))
            {
                var isSafe = false;
                for (var i = 0; i < report.Count - 1; i++)
                {
                    var diff = Math.Abs(report[i] - report[i + 1]);
                    if (diff >= 1 && diff <= 3)
                        isSafe = true;
                    else
                    {
                        isSafe = false;
                        break;
                    }
                }

                return isSafe;
            }
            return false;
        }

        private static bool IsSafe(int item1, int item2, bool isDescending)
        {
            if (item1 < item2 && isDescending)
                return false;
            else if (item1 > item2 && !isDescending)
                return false;

            var diff = Math.Abs(item1 - item2);
            return diff >= 1 && diff <= 3;
        }
    }
}
