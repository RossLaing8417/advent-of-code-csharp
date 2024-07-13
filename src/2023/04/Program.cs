namespace AoC2023
{
    class Day04
    {

        static void Main()
        {
            var source = File.ReadAllText("input.txt");

            Console.WriteLine($"Part 1: {Part1(source)}");
            Console.WriteLine($"Part 2: {Part2(source)}");
        }

        static int Part1(string source)
        {
            var lines = source.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;

            var winning_nums = new List<string>(10);

            foreach (var line in lines)
            {
                winning_nums.Clear();

                var nums = line.Split(':')[1].Split('|', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                var score = -1;

                nums[0].Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries)
                    .ToList()
                    .ForEach(n => { if (n.Length > 0) winning_nums.Add(n); });

                nums = nums[1].Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                foreach (var num in nums)
                {
                    if (winning_nums.Contains(num))
                    {
                        score *= (score == -1 ? -1 : 2);
                    }
                }

                if (score != -1)
                {
                    sum += score;
                }
            }

            return sum;
        }

        static int Part2(string source)
        {
            var lines = source.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;

            var winning_nums = new List<string>(10);
            var counts = new int[lines.Length];
            var index = 0;

            foreach (var line in lines)
            {
                winning_nums.Clear();

                var nums = line.Split(':')[1].Split('|', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                var count = 0;

                nums[0].Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries)
                    .ToList()
                    .ForEach(n => { if (n.Length > 0) winning_nums.Add(n); });

                nums = nums[1].Split(' ', StringSplitOptions.RemoveEmptyEntries & StringSplitOptions.TrimEntries);
                foreach (var num in nums)
                {
                    if (winning_nums.Contains(num))
                    {
                        count += 1;
                    }
                }

                counts[index] += 1;
                for (int i = 1; i <= count; i++)
                {
                    counts[index + i] += counts[index];
                }

                index += 1;
            }

            sum = counts.Sum();

            return sum;
        }

    }
}
