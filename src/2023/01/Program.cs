namespace AoC2023
{
    class Day01
    {

        static void Main()
        {
            var source = File.ReadAllText("input.txt");

            Console.WriteLine($"Part 1: {Part1(source)}");
            Console.WriteLine($"Part 2: {Part2(source)}");
        }

        static int Part1(string source)
        {
            var lines = source.Split('\n');
            int sum = 0;

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                int first = line.First(c => char.IsDigit(c)) - '0';
                int last = line.Last(c => char.IsDigit(c)) - '0';

                sum += (first * 10) + last;
            }

            return sum;
        }


        static int Part2(string source)
        {
            var lines = source.Split('\n');
            int sum = 0;
            var words = new Dictionary<string, int>()
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
            };

            for (int i = 0; i < 10; i++)
            {
                words.Add(i.ToString(), i);
            }

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                int first = 0, first_index = line.Length;
                int last = 0, last_index = -1;
                int index;

                foreach (var word in words)
                {
                    index = line.IndexOf(word.Key);
                    if (index < 0)
                    {
                        continue;
                    }

                    if (index < first_index)
                    {
                        first_index = index;
                        first = word.Value;
                    }

                    index = line.LastIndexOf(word.Key);
                    if (index > last_index)
                    {
                        last_index = index;
                        last = word.Value;
                    }
                }

                sum += (first * 10) + last;
            }

            return sum;
        }

    }
}
