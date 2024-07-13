namespace AoC2023
{
    class Day06
    {

        static void Main()
        {
            var source = File.ReadAllText("input.txt");

            Console.WriteLine($"Part 1: {Part1(source)}");
            Console.WriteLine($"Part 2: {Part2(source)}");
        }

        static long Part1(string data)
        {
            var lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            long result = 1;

            var times = lines[0].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var distances = lines[1].Split(':')[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < times.Length; i++)
            {
                var time = int.Parse(times[i]);
                var record = int.Parse(distances[i]);
                var times_won = 0;

                for (int ms = 0; ms < time; ms++)
                {
                    var distance = ms * (time - ms);
                    if (distance > record)
                    {
                        times_won += 1;
                    }
                }

                result *= times_won;
            }

            return result;
        }

        static long Part2(string data)
        {
            var lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            long result = 0;

            var times = lines[0].Split(':')[1].Replace(" ", "");
            var distances = lines[1].Split(':')[1].Replace(" ", "");

            var time = long.Parse(times);
            var record = long.Parse(distances);

            for (int ms = 0; ms < time; ms++)
            {
                var distance = ms * (time - ms);
                if (distance > record)
                {
                    result += 1;
                }
            }

            return result;
        }

    }
}
