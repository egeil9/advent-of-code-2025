using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025.Days
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

        public long Day_02_Solve_02()
        {
            long invalidTotal = 0;
            long min;
            long max;

            foreach (var range in _input)
            {
                min = long.Parse(range.Substring(0, range.IndexOf("-")));
                max = long.Parse(range.Substring(range.IndexOf("-") + 1));
                HashSet<long> potentialInvalidIds = new HashSet<long>();

                GetInvalidIds_02(min.ToString(), true, max, potentialInvalidIds);
                if(min.ToString().Length != max.ToString().Length) GetInvalidIds_02(max.ToString(), false, min, potentialInvalidIds);
                foreach(var id in potentialInvalidIds)
                {
                    if (id.ToString().Length > 1 && id >= min && id <= max) invalidTotal += id;
                }
            }
            return invalidTotal;
        }

        public void GetInvalidIds_02(string startingValue, bool add, long endOfRange, HashSet<long> invalidIds)
        {
            List<long> sequences = new List<long>() { long.Parse(startingValue.Substring(0, 1)) };
            for(int i = 2; i < (startingValue.Length / 2) + 1; i++)
            {
                if(startingValue.Length % i == 0)
                {
                    sequences.Add(long.Parse(startingValue.Substring(0, startingValue.Length / i)));
                }
            }

            for(int i = 0; i < sequences.Count; i ++)
            {
                var inRange = true;
                var sequenceLength = sequences[i].ToString().Length;

                while(inRange && sequences[i].ToString().Length == sequenceLength)
                {
                    var id = sequences[i];
                    while (id.ToString().Length < startingValue.Length)
                    {
                        id *= Convert.ToInt64(Math.Pow(10, sequences[i].ToString().Length));
                        id += sequences[i];
                    }

                    if (add && id > endOfRange || !add && id < endOfRange)
                    {
                        inRange = false;
                    }
                    else
                    {
                        invalidIds.Add(id);
                    }
                    sequences[i] = add ? sequences[i] + 1 : sequences[i] - 1;
                    if (sequences[i] == 0) inRange = false;
                }
            }
        }
    }
}
