namespace AoC2023
{
    class Day05
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
            long result = long.MaxValue;

            var seeds = new List<long>();
            var mapping_field = "";

            var mapping = new Dictionary<string, List<Range>>(){
                { "seed-to-soil", new List<Range>() },
                { "soil-to-fertilizer", new List<Range>() },
                { "fertilizer-to-water", new List<Range>() },
                { "water-to-light", new List<Range>() },
                { "light-to-temperature", new List<Range>() },
                { "temperature-to-humidity", new List<Range>() },
                { "humidity-to-location", new List<Range>() },
            };

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                if (line.StartsWith("seeds:"))
                {
                    var split = line.Split(":")[1].TrimStart().Split(' ');
                    seeds.EnsureCapacity(split.Length);

                    foreach (var seed in split)
                    {
                        seeds.Add(long.Parse(seed));
                    }

                    continue;
                }

                if (char.IsLetter(line[0]))
                {
                    mapping_field = line.Split(' ')[0];
                    continue;
                }

                var items = line.Split(' ');
                mapping[mapping_field].Add(new Range
                {
                    destinitation_start = long.Parse(items[0]),
                    source_start = long.Parse(items[1]),
                    length = long.Parse(items[2]),
                });
            }

            foreach (var seed in seeds)
            {
                var source = seed;
                var destination = seed;

                foreach (var entry in mapping)
                {
                    foreach (var range in entry.Value)
                    {
                        source = destination;

                        var start = range.source_start;
                        var end = range.source_start + range.length;

                        if (source >= start && source < end)
                        {
                            destination = range.destinitation_start + source - start;
                            break;
                        }
                    }
                }

                if (destination < result)
                {
                    result = destination;
                }
            }

            return result;
        }

        static long Part2(string data)
        {
            var lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            long result = long.MaxValue;

            SeedRange[] seed_ranges = { };
            var mapping_field = "";

            var mapping = new Dictionary<string, List<Range>>(){
                { "seed-to-soil", new List<Range>() },
                { "soil-to-fertilizer", new List<Range>() },
                { "fertilizer-to-water", new List<Range>() },
                { "water-to-light", new List<Range>() },
                { "light-to-temperature", new List<Range>() },
                { "temperature-to-humidity", new List<Range>() },
                { "humidity-to-location", new List<Range>() },
            };

            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    continue;
                }

                if (line.StartsWith("seeds:"))
                {
                    var seeds = line.Split(":")[1].TrimStart().Split(' ');
                    seed_ranges = new SeedRange[seeds.Length / 2];

                    for (int i = 0; i < seeds.Length / 2; i++)
                    {
                        seed_ranges[i] = new SeedRange
                        {
                            start = long.Parse(seeds[(i * 2) + 0]),
                            length = long.Parse(seeds[(i * 2) + 1]),
                            min = long.MaxValue,
                            scale = 1_000_000,
                        };
                    }

                    continue;
                }

                if (char.IsLetter(line[0]))
                {
                    mapping_field = line.Split(' ')[0];
                    continue;
                }

                var items = line.Split(' ');
                mapping[mapping_field].Add(new Range
                {
                    destinitation_start = long.Parse(items[0]),
                    source_start = long.Parse(items[1]),
                    length = long.Parse(items[2]),
                });
            }

            for (var i = 0; i < seed_ranges.Length; i += 1)
            {
                ref var seed_range = ref seed_ranges[i];
                long lowest_seed = 0;

                while (seed_range.length > 0)
                {
                    for (var seed = seed_range.start; seed < seed_range.start + seed_range.length; seed += seed_range.scale)
                    {
                        var source = seed;
                        var destination = seed;

                        foreach (var entry in mapping)
                        {
                            foreach (var range in entry.Value)
                            {
                                source = destination;

                                var start = range.source_start;
                                var end = range.source_start + range.length;

                                if (source >= start && source < end)
                                {
                                    destination = range.destinitation_start + source - start;
                                    break;
                                }
                            }
                        }

                        if (destination < seed_range.min)
                        {
                            seed_range.min = destination;
                            lowest_seed = seed;
                        }
                    }

                    seed_range.start = long.Max(lowest_seed - seed_range.scale, seed_range.start);

                    if (seed_range.scale == 1)
                    {
                        seed_range.length = 0;
                    }
                    else
                    {
                        seed_range.length = long.Min(seed_range.scale * 2, seed_range.length);
                    }

                    seed_range.scale /= 10;
                }
            }

            foreach (var seed_range in seed_ranges)
            {
                if (seed_range.min < result)
                {
                    result = seed_range.min;
                }
            }

            return result;
        }

        struct Range
        {
            public long destinitation_start;
            public long source_start;
            public long length;
        }

        record struct SeedRange
        {
            public long start;
            public long length;
            public long min;
            public long scale;
        }

    }
}
