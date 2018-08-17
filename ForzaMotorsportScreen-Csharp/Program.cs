
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace ForzaMotorsportScreen_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 1, 1, 2, 2, 4};

            // Test For Loop Solution
            PrintSumCombinationsForLoop(input.ToList<int>(), 4);

            // Test Recursive Solution
            PrintSumCombinations(input.ToList<int>(), 4);

            // Test possible Invalid Inputs
            WriteLine("\n--------------------------------------------------------\nTesting Invalid Inputs\n");
            PrintSumCombinations(new List<int>(), 4);
            PrintSumCombinationsForLoop(new List<int>(), 4);
            PrintSumCombinations(null, 4);
            PrintSumCombinationsForLoop(null, 4);
            
            // Additional Tests..
            WriteLine("\n--------------------------------------------------------");
            PrintSumCombinationsForLoop(new List<int>() { 2, 0, 2 }, 4);
            PrintSumCombinations(new List<int>() { 2, 0, 2 }, 4);

            WriteLine("\n--------------------------------------------------------");
            PrintSumCombinationsForLoop(new List<int>() { 1, 3, 1, 1, 1 }, 4);
            PrintSumCombinations(new List<int>() { 1, 3, 1, 1, 1 }, 4);

            // This test demonstrates a flaw in the Recursive Solution when
            // the value of the last index is 0.
            WriteLine("\n--------------------------------------------------------");
            PrintSumCombinationsForLoop(new List<int>() { 0, 4, 4 }, 4);
            PrintSumCombinations(new List<int>() { 0, 4, 4}, 4);

            ReadLine();
        }

        public static void PrintSumCombinations(List<int> numbers, int n)
        {
            if (!(numbers?.Count > 0))
            {
                return;
            }

            bool[] subArray = new bool[numbers.Count];

            //Recursive(index: 0, currentSum: 0, targetSum:n, subArray:subArray, array:numbers);

            var indexes = new List<int>();

            WriteLine(); 
            Recursive(index: 0, currentSum: 0, targetSum: n, indexes: indexes, inputArray: numbers);
        }

        public static void PrintSumCombinationsForLoop(List<int> numbers, int targetSum)
        {
            if (numbers == null || numbers.Count < 1)
                return;

            var ba = new BitArray(numbers.Count);
            var m  = Math.Pow(2, numbers.Count) - 1;

            // Loop through all possible bit value combinations
            for (int index = 0; index < m; index++)
            {
                ba = (index+1).ToBitArray(numbers.Count);

                // Print if the subset of values equals the target sum.
                if (targetSum == SubsetSum(ba, numbers))
                {
                    Print(ba, numbers);
                }
            }

        }

        static int SubsetSum(BitArray bArray, List<int> numbers)
        {
            int sum = 0;

            for (int index=0; index < bArray.Count; index++)
            {
                if (bArray[index])
                {
                    sum += numbers[index];
                }
            }

            return sum;
        }

        static void Recursive(int index, int currentSum, int targetSum, bool[] subArray, List<int> array)
        {

            if (currentSum == targetSum)
            {
                Print(subArray, array);
            }
            else if (index == array.Count)
            {
                return;
            }
            else
            { 
                if (array[index] != 0) subArray[index] = true;
                currentSum += array[index];
                Recursive(index + 1, currentSum, targetSum, subArray, array);

                subArray[index] = false;
                currentSum -= array[index];
                Recursive(index + 1, currentSum, targetSum, subArray, array);
            }
        }

        static void Recursive(int index, int currentSum, int targetSum, List<int> indexes, List<int> inputArray)
        {

            if (currentSum == targetSum)
            {
                Print(indexes, inputArray);
            }
            else if (index == inputArray.Count)
            {
                return;
            }
            else
            {
                currentSum += inputArray[index];
                if (inputArray[index] != 0) indexes.Add(index);
                Recursive(index + 1, currentSum, targetSum, indexes, inputArray);


                currentSum -= inputArray[index];
                if(inputArray[index] != 0) indexes.RemoveAt(indexes.Count - 1);
                Recursive(index + 1, currentSum, targetSum, indexes, inputArray);
            }
        }

        static void Print(bool[] subArray, List<int> array)
        {
            var sb = new StringBuilder();

            sb.Append("Indexes: [");
            for (int i=0; i< subArray.Length; i++)
            {
                if (subArray[i])
                {
                    sb.Append($"{i} ");
                }
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            var sbLen = sb.Length;

            // Add some padding..
            if (sbLen < 40)
            {
                sb.Append(' ', 20 - sbLen);
            }

            sb.Append("Values:  [");
            for (int i = 0; i < subArray.Length; i++)
            {
                if (subArray[i])
                {
                    sb.Append($"{array[i]} ");
                }
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            WriteLine(sb.ToString());
        }

        static void Print(List<int> indexes, List<int> array)
        {
            var sb = new StringBuilder();

            sb.Append("Indexes: [");
            for (int i = 0; i < indexes.Count; i++)
            {
                sb.Append($"{indexes[i]} ");
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            var sbLen = sb.Length;

            // Add some padding..
            if (sbLen < 40)
            {
                sb.Append(' ', 20 - sbLen);
            }

            sb.Append("Values:  [");
            for (int i = 0; i < indexes.Count; i++)
            {
                sb.Append($"{array[indexes[i]]} ");
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            WriteLine(sb.ToString());
        }

        static void Print(BitArray bArray, List<int> array)
        {
            var sb = new StringBuilder();

            sb.Append("Indexes: [");
            for (int i = 0; i < bArray.Length; i++)
            {
                if (bArray[i])
                {
                    sb.Append($"{i} ");

                }
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            var sbLen = sb.Length;

            // Add some padding..
            if (sbLen < 40)
            {
                sb.Append(' ', 20 - sbLen);
            }

            sb.Append("Values:  [");
            for (int i = 0; i < bArray.Length; i++)
            {
                if (bArray[i])
                {
                    sb.Append($"{array[i]} ");
                }
            }
            sb.Remove(sb.Length - 1, 1); // Remove trailing space
            sb.Append("]");

            WriteLine(sb.ToString());
        }
    }

    public static class Int32Extension
    {
        /// <summary>
        /// Convert the passed in int32 value into its BitArray representation
        /// using little Endian encoding.
        /// </summary>
        /// <param name="value">The value to covert into a BitArray representation</param>
        /// <param name="size">The size of the BitArray to return</param>
        /// <returns>A BitArray representation of an int32 value</returns>
        public static BitArray ToBitArray(this int value, int size)
        {
            var bArray = new BitArray(size);

            int m = (int)Math.Pow(2, bArray.Count - 1);

            for (int index = bArray.Count - 1; index > -1; index--, m /= 2)
            {
                bArray[index] = (m & value) > 0;
            }

            return bArray;
        }


    }

    //
    //           Problem Descrition
    //
    /*
            Using the following function signature, write a C# function that prints out every combination of indices using Console.WriteLine() whose values add up to a specified sum, n. Values of 0 should be ignored.

            public void PrintSumCombinations(List<int> numbers, int n);

            •	It’s okay to use additional private functions to implement the public function
            •	Be sure to print out the indices of numbers and not the values at those indices
            •	Don’t worry too much about memory or CPU optimization; focus on correctness

            To help clarify the problem, calling the function with the following input:

            List<int> numbers = new List<int> { 1, 1, 2, 2, 4 };
            PrintSumCombinations(numbers, 4);

            Should result in the following console output (the ordering of the different lines isn’t important and may vary by implementation):

            0 1 2 (i.e. numbers[0] + numbers[1] + numbers[2] = 1 + 1 + 2 = 4)
            0 1 3
            2 3
            4

     */
}
