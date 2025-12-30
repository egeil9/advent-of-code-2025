using AoCHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025.Days
{
    internal class Day_05: BaseDay
    {
        private readonly string[] _input;
        private readonly int _splitIndex;

        public override ValueTask<string> Solve_1() => new($"{Day_05_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_05_Solve_02()}");


        public Day_05()
        {
            _input = File.ReadAllLines(InputFilePath);
            _splitIndex = _input.IndexOf("");
        }

        public int Day_05_Solve_01()
        {
            int freshCount = 0;
            var rangeStarts = new List<long>(_splitIndex);
            var rangeEnds = new List<long>(_splitIndex);

            for (int i = 0; i < _splitIndex; i++)
            {
                int dashIndex = _input[i].IndexOf("-");

                rangeStarts.Add(long.Parse(_input[i].Substring(0, dashIndex)));
                rangeEnds.Add(long.Parse(_input[i].Substring(dashIndex + 1)));
            }

            for (int i = _splitIndex + 1; i < _input.Length; i++)
            {
                bool matchNotFound = true;
                int j = 0;
                while(matchNotFound && j < _splitIndex)
                {
                    long.TryParse(_input[i], out var id);
                    if (id >= rangeStarts[j] && id <= rangeEnds[j])
                    {
                        freshCount++;
                        matchNotFound = false;
                    }
                    j++;
                }
            }

            return freshCount;
        }

        public long Day_05_Solve_02()
        {
            long freshCount = 0;
            var rangeStarts = new List<long>(_splitIndex);
            var rangeEnds = new List<long>(_splitIndex);

            for (int i = 0; i < _splitIndex; i++)
            {
                int dashIndex = _input[i].IndexOf("-");

                rangeStarts.Add(long.Parse(_input[i].Substring(0, dashIndex)));
                rangeEnds.Add(long.Parse(_input[i].Substring(dashIndex + 1)));
            }

            for (int i = 0; i < rangeStarts.Count; i ++)
            {
                var range = new KeyValuePair<long, long>(rangeStarts[i], rangeEnds[i]);
                bool skipRange = false;

                for (int j = i + 1; j < rangeStarts.Count; j++)
                {
                    var compareRange = new KeyValuePair<long, long>(rangeStarts[j], rangeEnds[j]);

                    if (rangeStarts[j] >= rangeStarts[i] && rangeEnds[j] <= rangeEnds[i])
                    {
                        // handle total overlap - compared range entirely inside range
                        rangeStarts.RemoveAt(j);
                        rangeEnds.RemoveAt(j);
                    } else if (rangeStarts[i] >= rangeStarts[j] && rangeEnds[i] <= rangeEnds[j])
                    {
                        // handle range entirely inside compared range
                        skipRange = true;
                    }
                }

                if (!skipRange)
                {
                    freshCount += rangeEnds[i] - rangeStarts[i] + 1;

                    for (int j = i + 1; j < rangeStarts.Count; j++)
                    {
                        var compareRange = new KeyValuePair<long, long>(rangeStarts[j], rangeEnds[j]);

                        if (rangeStarts[i] >= rangeStarts[j] && rangeStarts[i] <= rangeEnds[j])
                        {
                            if (rangeStarts[i] == rangeEnds[i] && rangeStarts[i] == rangeStarts[j])
                            {
                                rangeStarts[j]++;
                            }
                            else
                            {
                                rangeEnds[j] = rangeStarts[i] - 1;
                            }
                            compareRange = new KeyValuePair<long, long>(rangeStarts[j], rangeEnds[j]);
                        }
                        if (rangeEnds[i] >= rangeStarts[j] && rangeEnds[i] <= rangeEnds[j])
                        {
                            rangeStarts[j] = rangeEnds[i] + 1;
                            compareRange = new KeyValuePair<long, long>(rangeStarts[j], rangeEnds[j]);
                        }
                    }
                }
            }
            return freshCount;
        }
    }
}
