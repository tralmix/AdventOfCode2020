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
			int index = FindInvalidIndex(preamble, inputAsIntegers);

			Console.WriteLine($"First number to fail is {inputAsIntegers[index]}");
		}

		private static int FindInvalidIndex(int preamble, List<long> inputAsIntegers)
		{
			var working = true;
			var index = preamble;
			do
			{
				if (!inputAsIntegers[index].IsSum(inputAsIntegers.GetRange(index - preamble, preamble)))
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
		public static bool IsSum(this long value, List<long> options)
		{
			if (options.Any(x => options.Any(y => x != y && x + y == value)))
				return true;
			return false;
		}

	}
}
