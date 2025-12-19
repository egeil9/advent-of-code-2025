using AoCHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace advent_of_code_2025.Days
{
    public class Day_04 : BaseDay
    {
        private readonly string[][] _input;
        private readonly string _paperRoll;
        private int[,] _surroundingRolls;
        public override ValueTask<string> Solve_1() => new($"{Day_04_Solve_01()}");
        public override ValueTask<string> Solve_2() => new($"{Day_04_Solve_02()}");


        public Day_04()
        {
            _input = File.ReadAllLines(InputFilePath).
                Select(line => line.Select(letter => letter.ToString()).ToArray()).ToArray();
            _paperRoll = "@";
            _surroundingRolls = GetSurroundingRolls();
        }

        public int Day_04_Solve_01()
        {
            int moveableRolls = 0;

            for (int row = 0; row < _input.Length; row++)
            {
                for (int column = 0; column < _input[row].Length; column++)
                {
                    if (_input[row][column].Equals(_paperRoll) && _surroundingRolls[row, column] < 4) moveableRolls++;
                }
            }

            return moveableRolls;
        }

        public int Day_04_Solve_02()
        {
            int moveableRolls = 0;

            bool rollsToRemove = true;
            while (rollsToRemove)
            {
                rollsToRemove = false;
                for (int row = 0; row < _input.Length; row++)
                {
                    for (int column = 0; column < _input[row].Length; column++)
                    {
                        if (_input[row][column].Equals(_paperRoll) && _surroundingRolls[row, column] < 4)
                        {
                            rollsToRemove = true;
                            moveableRolls++;
                            // Change item into a non-roll
                            _input[row][column] = ".";
                            // Decrement the surrounding rolls count of neighboring spots
                            SetSurroundingRolls(row, column, _surroundingRolls, false);
                        }
                    }
                }
            }
            return moveableRolls;
        }


        public int[,] GetSurroundingRolls()
        {
            int[,] surroundingRolls = new int[_input.Length, _input[0].Length];

            for (int row = 0; row < _input.Length; row++)
            {
                for (int column = 0; column < _input[row].Length; column++)
                {
                    if (_input[row][column].Equals(_paperRoll))
                    {
                        SetSurroundingRolls(row, column, surroundingRolls, true);
                    }
                }
            }

            return surroundingRolls;
        }
        public void SetSurroundingRolls(int row, int column, int[,] surroundingRolls, bool add)
        {
            int value = add ? 1 : -1;
            if (row > 0)
            {
                if (column > 0) surroundingRolls[row - 1, column - 1]+= value;
                if (column < _input[row].Length - 1) surroundingRolls[row - 1, column + 1] += value;
                surroundingRolls[row - 1, column] += value;
            }

            if (row < _input.Length - 1)
            {
                if (column > 0) surroundingRolls[row + 1, column - 1] += value;
                if (column < _input[row].Length - 1) surroundingRolls[row + 1, column + 1] += value;
                surroundingRolls[row + 1, column] += value;
            }

            if (column > 0) surroundingRolls[row, column - 1] += value;
            if (column < _input[row].Length - 1) surroundingRolls[row, column + 1] += value;
        }
    }
}
