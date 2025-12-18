using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025.Days
{
    public class Day_03 : BaseDay
    {
        private readonly int[][] _input;
        public override ValueTask<string> Solve_1() => new($"{Day_03_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_03_Solve_02()}");


        public Day_03()
        {
            _input = File.ReadAllLines(InputFilePath).
                Select(line => line.Select(letter => int.Parse(letter.ToString())).ToArray()).ToArray();
        }

        public int Day_03_Solve_01()
        {
            int totalJoltage = 0;

            foreach(var batteryBank in _input)
            {
                int tensIndex = 0;
                int onesIndex = batteryBank.Length - 1;

                for(int i = 1; i <= batteryBank.Length - 2; i++)
                {
                    if (batteryBank[i] > batteryBank[tensIndex]) tensIndex = i;
                }
                for (int j = tensIndex + 1; j < batteryBank.Length; j++)
                {
                    if (batteryBank[j] > batteryBank[onesIndex]) onesIndex = j;
                }

                totalJoltage += batteryBank[tensIndex] * 10 + batteryBank[onesIndex];
            }

            return totalJoltage;
        }

        public int Day_03_Solve_02()
        {
            return -1;
        }
    }
}
