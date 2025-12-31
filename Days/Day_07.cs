using AoCHelper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace advent_of_code_2025.Days
{
    public class Day_07 : BaseDay
    {
        public string[] _input;
        public override ValueTask<string> Solve_1() => new($"{Day_07_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_07_Solve_02()}");

        public Day_07()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public struct Snapshot
        {
            public int beamIndex;
            public int timelines;
            public int stage;
            public string[] input;
        }

        public int Day_07_Solve_01()
        {
            var beams = new HashSet<int> { _input[0].IndexOf("S") };
            int splitCount = 0;

            for(int i = 1; i < _input.Length; i++)
            {
                var splitters = Enumerable.Range(0, _input[i].Length).Where(x => _input[i].Substring(x,1) == "^").ToArray();
                foreach(var splitIndex in splitters)
                {
                    if (beams.Contains(splitIndex))
                    {
                        splitCount++;
                        beams.Remove(splitIndex);
                        if (splitIndex - 1 > 0) beams.Add(splitIndex - 1);
                        if (splitIndex + 1 < _input[i].Length) beams.Add(splitIndex + 1);
                    }
                }
            }

            return splitCount;
        }

        public long Day_07_Solve_02()
        {
            var totals = new long[_input[0].Length];

            var beams = new HashSet<int> { _input[0].IndexOf("S") };
            totals[_input[0].IndexOf("S")] = 1;

            for (int i = 1; i < _input.Length; i++)
            {
                var splitters = Enumerable.Range(0, _input[i].Length).Where(x => _input[i].Substring(x, 1) == "^").ToArray();
                if(splitters.Length > 0)
                {
                    foreach (var splitIndex in splitters)
                    {
                        if (beams.Contains(splitIndex))
                        {
                            beams.Remove(splitIndex);
                            if (splitIndex - 1 > 0) beams.Add(splitIndex - 1);
                            if (splitIndex + 1 < _input[i].Length) beams.Add(splitIndex + 1);
                            totals[splitIndex + 1] += totals[splitIndex];
                            totals[splitIndex - 1] += totals[splitIndex];
                            totals[splitIndex] = 0;
                        }
                    }
                }
            }
            return totals.Sum();
        }
    }
}
