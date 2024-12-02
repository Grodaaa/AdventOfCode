using System.Reflection;
using System.Text;

namespace AdventOfCode
{
    abstract class DailyTask
    {
        protected string Input { get; }

        protected DailyTask()
        {
            Input = LoadInput();
        }

        public abstract string PartOne();
        public abstract string PartTwo();

        private string LoadInput()
        {
            var info = Assembly.GetExecutingAssembly().GetName();
            var name = info.Name;
            //var inputFileName = $"{name}.inputs.{GetType().Name.ToLower()}_input.txt";
            var inputFileName = $"AdventOfCode.inputs.{GetType().Name.ToLower()}_input.txt";

            var hej = Assembly
            .GetExecutingAssembly().GetManifestResourceNames();

            using var stream = Assembly
                        .GetExecutingAssembly()
                        .GetManifestResourceStream(inputFileName)!;

            using var streamReader = new StreamReader(stream, Encoding.UTF8);

            return streamReader.ReadToEnd();
        }
    }
}
