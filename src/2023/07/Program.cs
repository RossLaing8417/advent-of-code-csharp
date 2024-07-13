namespace AoC2023
{
    class Day07
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
            long result = 0;

            var hands = new List<Hand>(lines.Length);

            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var hand = split[0];

                hands.Add(new Hand(hand, long.Parse(split[1])));
            }

            hands.Sort();

            for (int i = 0; i < hands.Count; i++)
            {
                result += hands[i].bid * (i + 1);
            }

            return result;
        }

        static long Part2(string data)
        {
            var lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            long result = 0;

            var hands = new List<Hand2>(lines.Length);

            foreach (var line in lines)
            {
                var split = line.Split(' ');
                var hand = split[0];

                hands.Add(new Hand2(hand, long.Parse(split[1])));
            }

            hands.Sort();

            for (int i = 0; i < hands.Count; i++)
            {
                result += hands[i].bid * (i + 1);
            }

            return result;
        }

        record struct Hand : IComparable<Hand>
        {
            public string hand;
            public HandType type;
            public long bid;

            public Hand(string hand, long bid)
            {
                this.hand = hand;
                this.bid = bid;

                var map = new int[255];
                var counts = new int[6];

                foreach (var c in hand)
                {
                    map[c] += 1;
                }

                foreach (var count in map)
                {
                    if (count > 0)
                    {
                        counts[count] += 1;
                    }
                }

                this.type = (counts[2], counts[3], counts[4], counts[5]) switch
                {
                    (_, _, _, 1) => HandType.five_of_a_kind,
                    (_, _, 1, _) => HandType.four_of_a_kind,
                    (1, 1, _, _) => HandType.full_house,
                    (_, 1, _, _) => HandType.three_of_a_kind,
                    (2, _, _, _) => HandType.two_pair,
                    (1, _, _, _) => HandType.one_pair,
                    (_, _, _, _) => HandType.high_card,
                };
            }

            public int CompareTo(Hand other)
            {
                if (this.type != other.type)
                {
                    return this.type.CompareTo(other.type);
                }

                for (int i = 0; i < this.hand.Length; i++)
                {
                    if (this.hand[i] != other.hand[i])
                    {
                        return CardWeight(this.hand[i]).CompareTo(CardWeight(other.hand[i]));
                    }
                }

                return 0;
            }

            public static int CardWeight(char card) => card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                _ => card - '0',
            };
        }

        record struct Hand2 : IComparable<Hand2>
        {
            public string hand;
            public HandType type;
            public long bid;

            public Hand2(string hand, long bid)
            {
                this.hand = hand;
                this.bid = bid;

                var map = new int[255];
                var counts = new int[6];
                var joker_count = 0;

                foreach (var c in hand)
                {
                    if (c == 'J')
                    {
                        joker_count += 1;
                    }
                    else
                    {
                        map[c] += 1;
                    }
                }

                var max_count = 0;
                foreach (var count in map)
                {
                    if (count > 0)
                    {
                        max_count = int.Max(max_count, count);
                        counts[count] += 1;
                    }
                }

                if (joker_count == 5)
                {
                    counts[joker_count] += 1;
                }
                else if (joker_count > 0)
                {
                    counts[max_count] -= 1;
                    counts[max_count + joker_count] += 1;
                }

                this.type = (counts[2], counts[3], counts[4], counts[5]) switch
                {
                    (_, _, _, 1) => HandType.five_of_a_kind,
                    (_, _, 1, _) => HandType.four_of_a_kind,
                    (1, 1, _, _) => HandType.full_house,
                    (_, 1, _, _) => HandType.three_of_a_kind,
                    (2, _, _, _) => HandType.two_pair,
                    (1, _, _, _) => HandType.one_pair,
                    (_, _, _, _) => HandType.high_card,
                };
            }

            public int CompareTo(Hand2 other)
            {
                if (this.type != other.type)
                {
                    return this.type.CompareTo(other.type);
                }

                for (int i = 0; i < this.hand.Length; i++)
                {
                    if (this.hand[i] != other.hand[i])
                    {
                        return CardWeight(this.hand[i]).CompareTo(CardWeight(other.hand[i]));
                    }
                }

                return 0;
            }

            public static int CardWeight(char card) => card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 0,
                'T' => 10,
                _ => card - '0',
            };
        }

        enum HandType
        {
            high_card,
            one_pair,
            two_pair,
            three_of_a_kind,
            full_house,
            four_of_a_kind,
            five_of_a_kind,
        }
    }
}
