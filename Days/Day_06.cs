using AoCHelper;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace advent_of_code_2025.Days
{
    public class Day_06 : BaseDay
    {
        public string[] _rawInput;
        public string[] _operators;

        public override ValueTask<string> Solve_1() => new($"{Day_06_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_06_Solve_02()}");

        public Day_06()
        {
            _rawInput = File.ReadAllLines(InputFilePath);
            _operators = _rawInput[_rawInput.Length - 1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        }

        public long Day_06_Solve_01()
        {
            var input = new int[_rawInput.Length - 1, _rawInput[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length];
            for (int i = 0; i < _rawInput.Length - 1; i++)
            {
                var line = _rawInput[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < line.Length; j++)
                {
                    input[i, j] = int.Parse(line[j]);
                }
            }

            long cephMathTotal = 0;

            for(int column = 0; column < input.GetLength(1); column++)
            {
                long columnTotal = input[0, column];
                for(int row = 1; row < input.GetLength(0); row++)
                {
                    columnTotal = PerformOperation(_operators[column], columnTotal, input[row, column]);
                }
                cephMathTotal += columnTotal;
            }

            return cephMathTotal;
        }

        public long Day_06_Solve_02()
        {
            var input = new string[_rawInput.Length, _rawInput[0].Length + 1];
            for (int i = 0; i < _rawInput.Length; i++)
            {

                string[] line = new string[0];
                if(i == _rawInput.Length - 1)
                {
                    line = _rawInput[i].Select(c => c.ToString()).ToArray();
                }
                else
                {
                    line = _rawInput[i].Split(" ");
                }

                var index = 0;
                for(int j = 0; j < line.Length; j++)
                {
                    if (line[j] == " " || line[j] == "+" || line[j] == "*")
                    {
                        input[i, index] = line[j];
                        index++;
                    }
                    else
                    {
                        for(int charPlace = 0; charPlace < line[j].Length; charPlace++)
                        {
                            input[i, index] = line[j].Substring(charPlace, 1);
                            index ++;
                        }
                        if(index < input.GetLength(1))
                        {
                            input[i, index] = " ";
                            index++;
                        }
                    }
                }
            }

            long cephMathTotal = 0;
            long equationTotal = 0;
            string op = "";

            for(int column = 0; column < input.GetLength(1); column++)
            {
                var num = GetNum(column, input);
                if(num > 0)
                {
                    if(equationTotal == 0)
                    {
                        equationTotal = num;
                        op = input[input.GetLength(0) - 1, column];
                    }
                    else
                    {
                        equationTotal = PerformOperation(op, equationTotal, num);
                    }
                }
                else
                {
                    cephMathTotal += equationTotal;
                    equationTotal = 0;
                }

            }

            return cephMathTotal;
        }

        public int GetNum(int column, string[,] input)
        {
            int value = -1;
            for(int row = 0; row < input.GetLength(0) - 1; row++)
            {
                if(input[row, column] != " ")
                {
                    if(value < 0)
                    {
                        value = int.Parse(input[row, column]);
                    }
                    else
                    {
                        value = value * 10 + int.Parse(input[row, column]);
                    }
                }
            }
            return value;
        }

        public long PerformOperation(string op, long num1, int num2)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "*":
                    return num1 * num2;
                default:
                    return -1;
            }
        }
    }
}
