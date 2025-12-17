using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025
{
    public class Day_02 : BaseDay
    {
        private readonly string[] _input;
        public override ValueTask<string> Solve_1() => new($"{Day_02_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_02_Solve_02()}");


        public Day_02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public int Day_02_Solve_01()
        {
            return -1;
        }

        public int Day_02_Solve_02()
        {
            return -1;
        }
    }
}
