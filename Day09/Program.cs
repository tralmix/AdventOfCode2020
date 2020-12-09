using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day09
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const int preamble = 25;
			var inputLines = await ReadInputLines();

			var inputAsIntegers = inputLines.Select(long.Parse).ToList();

			// Part One
			int failedIndex = FindInvalidIndex(preamble, inputAsIntegers);

			Console.WriteLine($"First number to fail is {inputAsIntegers[failedIndex]}");

			// Part Two

			long minMaxSum = FindMinMaxSumFromRangeThatSumsToFailedIndex(inputAsIntegers, failedIndex);

			Console.WriteLine($"Sum of min max in range is {minMaxSum}");
		}

		private static long FindMinMaxSumFromRangeThatSumsToFailedIndex(List<long> inputAsIntegers, int failedIndex)
		{
			var index = 0;
			int innerIndex;
			var rangeFound = false;
			do
			{
				var workingValue = inputAsIntegers[index];
				innerIndex = index + 1;
				while (workingValue < inputAsIntegers[failedIndex])
				{
					workingValue += inputAsIntegers[innerIndex];
					innerIndex++;
				}

				if (workingValue == inputAsIntegers[failedIndex])
					rangeFound = true;
				else
					index++;
			} while (!rangeFound && index < failedIndex);

			var range = inputAsIntegers.GetRange(index, innerIndex - index);
			var minMaxSum = range.Min() + range.Max();
			return minMaxSum;
		}

		private static int FindInvalidIndex(int preamble, List<long> inputAsIntegers)
		{
			var working = true;
			var index = preamble;
			do
			{
				if (!inputAsIntegers[index].HasSum(inputAsIntegers.GetRange(index - preamble, preamble)))
					working = false;
				else
					index++;
			} while (index < inputAsIntegers.Count && working);
			return index;
		}

		private static async Task<List<string>> ReadInputLines()
		{
			var inputLines = new List<string>();

			using var reader = new System.IO.StreamReader("Input.txt");
			while (reader.Peek() > -1)
			{
				var lineItem = await reader.ReadLineAsync();

				inputLines.Add(lineItem);
			}

			return inputLines;
		}
	}

	public static class IntExtension
	{
		public static bool HasSum(this long value, List<long> options)
		{
			if (options.Any(x => options.Any(y => x != y && x + y == value)))
				return true;
			return false;
		}

	}
}
