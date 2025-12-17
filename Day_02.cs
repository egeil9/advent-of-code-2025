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
            _input = File.ReadAllText(InputFilePath).Split(",");
        }

        public long Day_02_Solve_01()
        {
            long invalidTotal = 0;
            long min;
            long max;

            foreach(var range in _input)
            {
                min = long.Parse(range.Substring(0, range.IndexOf("-")));
                max = long.Parse(range.Substring(range.IndexOf("-") + 1));
                List<long> potentialInvalidIds = new List<long>();

                if(min.ToString().Length % 2 == 0)
                {
                    potentialInvalidIds = GetInvalidIds(min.ToString(), true, max);
                }else if(max.ToString().Length % 2 == 0)
                {
                    potentialInvalidIds = GetInvalidIds(max.ToString(), false, min);
                }
                potentialInvalidIds.FindAll(id => id >= min && id <= max)?.ForEach(id => invalidTotal += id);
            }
            return invalidTotal;
        }

        public List<long> GetInvalidIds(string startingValue, bool add, long endOfRange)
        {
            List<long> invalidIds = new List<long>();
            long sequence = long.Parse(startingValue.Substring(0, startingValue.Length / 2));
            var inRange = true;
            long id = 0;

            while(inRange && sequence.ToString().Length == startingValue.Length / 2)
            {
                id = sequence * Convert.ToInt64(Math.Pow(10, startingValue.Length / 2)) + sequence;
                if (add && id > endOfRange || !add && id < endOfRange)
                {
                    inRange = false;
                }
                else
                {
                    invalidIds.Add(id);
                }
                sequence = add ? sequence + 1 : sequence - 1;
            }

            return invalidIds;
        }

        public int Day_02_Solve_02()
        {
            return -1;
        }
    }
}
