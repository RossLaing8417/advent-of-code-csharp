namespace AoC2023
{
    class Day02
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

            var cube_limits = new Dictionary<string, int>() {
                { "red", 12},
                { "green", 13},
                { "blue", 14},
            };
            var cube_counts = new Dictionary<string, int>() {
                { "red", 0},
                { "green", 0},
                { "blue", 0},
            };

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                var split = line.Split(':');
                var game_id = int.Parse(split[0].Split(' ')[1]);
                var skip = false;

                var rounds = split[1].Split(';');
                foreach (var round in rounds)
                {
                    cube_counts["red"] = cube_counts["green"] = cube_counts["blue"] = 0;

                    var draws = round.Split(',');
                    foreach (var draw in draws)
                    {
                        split = draw.Trim().Split(' ');
                        cube_counts[split[1]] += int.Parse(split[0]);
                        if (cube_counts[split[1]] > cube_limits[split[1]])
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                    {
                        break;
                    }
                }

                if (!skip)
                {
                    sum += game_id;
                }
            }

            return sum;
        }


        static int Part2(string source)
        {
            var lines = source.Split('\n');
            int sum = 0;

            var cube_counts = new Dictionary<string, int>() {
                { "red", 0},
                { "green", 0},
                { "blue", 0},
            };

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                var split = line.Split(':');
                var game_id = int.Parse(split[0].Split(' ')[1]);

                cube_counts["red"] = cube_counts["green"] = cube_counts["blue"] = 0;

                var rounds = split[1].Split(';');
                foreach (var round in rounds)
                {
                    var draws = round.Split(',');
                    foreach (var draw in draws)
                    {
                        split = draw.Trim().Split(' ');
                        cube_counts[split[1]] = int.Max(cube_counts[split[1]], int.Parse(split[0]));
                    }
                }

                var power = cube_counts["red"] *= cube_counts["green"] *= cube_counts["blue"];

                sum += power;
            }

            return sum;
        }

    }
}
