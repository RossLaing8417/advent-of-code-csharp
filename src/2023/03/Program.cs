namespace AoC2023
{
    class Day03
    {

        static void Main()
        {
            var source = File.ReadAllText("input.txt");

            Console.WriteLine($"Part 1: {Part1(source)}");
            Console.WriteLine($"Part 2: {Part2(source)}");
        }

        static int Part1(string source)
        {
            var line_length = source.IndexOf('\n');
            int sum = 0;

            int index = 0;
            int start_index = -1;
            bool is_part = false;
            foreach (var c in source)
            {
                if (char.IsDigit(c) && start_index == -1)
                {
                    start_index = index;
                }
                else if (start_index != -1 && !char.IsDigit(c))
                {
                    var number = source.Substring(start_index, index - start_index);
                    if (is_part)
                    {
                        sum += int.Parse(number);
                    }
                    start_index = -1;
                    is_part = false;
                }

                if (start_index != -1 && !is_part && isPartNumber(source, line_length, index))
                {
                    is_part = true;
                }

                index += 1;
            }

            return sum;
        }

        static bool isPartNumber(string source, int length, int index)
        {
            return isSymbol(source, length, (index - length - 1) - 1)
                || isSymbol(source, length, (index - length - 1))
                || isSymbol(source, length, (index - length - 1) + 1)
                || isSymbol(source, length, index - 1)
                || isSymbol(source, length, index + 1)
                || isSymbol(source, length, (index + length + 1) - 1)
                || isSymbol(source, length, (index + length + 1))
                || isSymbol(source, length, (index + length + 1) + 1);
        }

        static bool isSymbol(string source, int length, int index)
        {
            if (index < 0 || index >= source.Length) return false;
            if (source[index] == '.' || source[index] == '\n') return false;
            if (char.IsDigit(source[index])) return false;
            return true;
        }

        static int Part2(string source)
        {
            var line_length = source.IndexOf('\n');
            int sum = 0;

            var numbers = new List<Number>();

            int index = 0;
            int start_index = -1;
            bool is_part = false;
            foreach (var c in source)
            {
                if (char.IsDigit(c) && start_index == -1)
                {
                    start_index = index;
                }
                else if (start_index != -1 && !char.IsDigit(c))
                {
                    var number = source.Substring(start_index, index - start_index);
                    if (is_part)
                    {
                        numbers.Add(new Number { start_index = start_index, end_index = index });
                    }
                    start_index = -1;
                    is_part = false;
                }

                if (start_index != -1 && !is_part && isPartNumber2(source, line_length, index))
                {
                    is_part = true;
                }

                index += 1;
            }

            index = 0;
            foreach (var c in source)
            {
                if (c == '*')
                {
                    int top_left = index - line_length - 2;
                    var top_right = index - line_length;
                    // var mid_left = index - 1; // Don't need this as num.end is exclusive so comparing num.end == index is valid
                    var mid_right = index + 1;
                    var bot_left = index + line_length;
                    var bot_right = index + line_length + 2;

                    var count = 0;
                    var product = 1;
                    foreach (var number in numbers)
                    {
                        var top_range = (number.end_index > top_left && number.start_index <= top_right);
                        var mid_range = (number.end_index == index || number.start_index == mid_right);
                        var bot_range = (number.end_index > bot_left && number.start_index <= bot_right);

                        if (top_range || mid_range || bot_range)
                        {
                            count += 1;
                            product *= int.Parse(source.Substring(number.start_index, number.end_index - number.start_index));
                        }
                    }

                    if (count == 2)
                    {
                        sum += product;
                    }
                }

                index += 1;
            }

            return sum;
        }

        struct Number
        {
            public int start_index;
            public int end_index;
        }

        static bool isPartNumber2(string source, int length, int index)
        {
            return isSymbol2(source, length, (index - length - 1) - 1)
                || isSymbol2(source, length, (index - length - 1))
                || isSymbol2(source, length, (index - length - 1) + 1)
                || isSymbol2(source, length, index - 1)
                || isSymbol2(source, length, index + 1)
                || isSymbol2(source, length, (index + length + 1) - 1)
                || isSymbol2(source, length, (index + length + 1))
                || isSymbol2(source, length, (index + length + 1) + 1);
        }

        static bool isSymbol2(string source, int length, int index)
        {
            if (index < 0 || index >= source.Length) return false;
            return source[index] == '*';
        }

    }
}
