namespace AdventOfCode2022
{
    internal static class Day8
    {
        public static void Run(string path)
        {
            Console.WriteLine("Day Eight");
            List<string> input = FileHelper.ReadFileAsList(path);

            Solve(input);
        }

        private static void Solve(List<string> input)
        {
            Dictionary<(int, int), bool> trees = new();
            int visible = 0;

            for (int i = 0; i < input.Count; i++)
            {
                var array = input[i].ToCharArray();
                for (int j = 0; j < array.Length; j++)
                {
                    trees.Add((i, j), false);
                }
            }

            Left(input, trees, ref visible);
            Right(input, trees, ref visible);
            Top(input, trees, ref visible);
            Bottom(input, trees, ref visible);

            int max = 0;
            foreach (var tree in trees)
            {
                int left = 0, right = 0, top = 0, bottom = 0, current = 0;

                for (int i = tree.Key.Item1 + 1; i < input.Count; i++)
                {
                    int value1 = int.Parse(input[tree.Key.Item1][tree.Key.Item2].ToString());
                    int value2 = int.Parse(input[i][tree.Key.Item2].ToString());

                    if (value1 > value2)
                        left++;
                    else
                    {
                        left++;
                        break;
                    } 
                }

                for (int i = tree.Key.Item1 - 1; i > -1; i--)
                {
                    int value1 = int.Parse(input[tree.Key.Item1][tree.Key.Item2].ToString());
                    int value2 = int.Parse(input[i][tree.Key.Item2].ToString());

                    if (value1 > value2)
                        right++;
                    else
                    {
                        right++;
                        break;
                    }
                }

                for (int i = tree.Key.Item2 + 1; i < input[0].Length; i++)
                {
                    int value1 = int.Parse(input[tree.Key.Item1][tree.Key.Item2].ToString());
                    int value2 = int.Parse(input[tree.Key.Item1][i].ToString());

                    if (value1 > value2)
                        top++;
                    else
                    {
                        top++;
                        break;
                    }
                }

                for (int i = tree.Key.Item2 - 1; i > -1; i--)
                {
                    int value1 = int.Parse(input[tree.Key.Item1][tree.Key.Item2].ToString());
                    int value2 = int.Parse(input[tree.Key.Item1][i].ToString());

                    if (value1 > value2)
                        bottom++;
                    else
                    {
                        bottom++;
                        break;
                    }
                }

                current = left * right * top * bottom;
                if (current > max)
                    max = current;
            }

            Console.WriteLine(trees.Count(x => x.Value == true));
            Console.WriteLine(max);
        }

        private static void Left(List<string> input, Dictionary<(int, int), bool> trees, ref int visible)
        {
            int height = -1;

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    int value = (int)char.GetNumericValue(input[i][j]);
                    if (value > height)
                    {
                        if (trees[(i, j)] == true)
                        {
                            height = value;
                            continue;
                        }

                        visible++;
                        height = (int)char.GetNumericValue(input[i][j]);
                        trees[(i, j)] = true;
                    }
                }

                height = -1;
            }
        }

        private static void Right(List<string> input, Dictionary<(int, int), bool> trees, ref int visible)
        {
            int height = -1;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = input[i].Length - 1; j > -1; j--)
                {
                    int value = (int)char.GetNumericValue(input[i][j]);
                    if (value > height)
                    {
                        if (trees[(i, j)] == true)
                        {
                            height = value;
                            continue;
                        }


                        visible++;
                        height = (int)char.GetNumericValue(input[i][j]);
                        trees[(i, j)] = true;
                    }
                }

                height = -1;
            }
        }

        private static void Top(List<string> input, Dictionary<(int, int), bool> trees, ref int visible)
        {
            int height = -1;
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    int value = (int)char.GetNumericValue(input[j][i]);
                    if (value > height)
                    {
                        if (trees[(j, i)] == true)
                        {
                            height = value;
                            continue;
                        }

                        visible++;
                        height = (int)char.GetNumericValue(input[j][i]);
                        trees[(j, i)] = true;
                    }
                }

                height = -1;
            }
        }

        private static void Bottom(List<string> input, Dictionary<(int, int), bool> trees, ref int visible)
        {
            int height = -1;
            for (int i = input.Count - 1; i > -1; i--)
            {
                for (int j = input[i].Length - 1; j > -1; j--)
                {
                    int value = (int)char.GetNumericValue(input[j][i]);
                    if (value > height)
                    {
                        if (trees[(j, i)] == true)
                        {
                            height = value;
                            continue;
                        }

                        visible++;
                        height = (int)char.GetNumericValue(input[j][i]);
                        trees[(j, i)] = true;
                    }
                }

                height = -1;
            }
        }
    }
}