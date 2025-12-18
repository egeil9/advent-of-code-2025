using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025.Days
{
    public class Day_01 : BaseDay
    {
        private readonly string[] _input;
        public override ValueTask<string> Solve_1() => new($"{Day_01_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_01_Solve_02(_input)}");


        public Day_01()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public int Day_01_Solve_01()
        {
            var position = 50;
            var count = 0;
            var clicks = 0;

            foreach (var item in _input)
            {
                clicks = int.Parse(item.Substring(1));
                position = item.StartsWith('R') ? position + clicks : position - clicks;
                if (position % 100 == 0) count++;
            }

            return count;
        }

        public int Day_01_Solve_02(string[] input)
        {
            int position = 50;
            int count = 0;
            int clicks = 0;
            int leftClicksToZero = 50;
            int rightClicksToZero = 50;
            bool turnRight = false;

            foreach (string item in input)
            {
                turnRight = item.StartsWith('R');
                clicks = int.Parse(item.Substring(1));
                count += clicks / 100;
                clicks = clicks % 100;

                if(position != 0)
                {
                    if (turnRight && clicks >= rightClicksToZero) count++;
                    if (!turnRight && clicks >= leftClicksToZero) count++;
                }

                position = GetPosition(turnRight ? clicks : -clicks, position);
                rightClicksToZero = position == 0 ? 0 : 100 - position;
                leftClicksToZero = position;
            }

            return count;
        }

        public int GetPosition(int clicks, int oldPosition)
        {
            if (clicks > 0) return (oldPosition + clicks) % 100;
            var position = oldPosition + clicks;
            if (position < 0) return 100 + position;
            return position;
        }
    }
}
