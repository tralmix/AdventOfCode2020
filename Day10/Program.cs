using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day10
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var inputLines = await ReadInputLines();

			var inputAsOrderedIntegers = inputLines.Select(int.Parse).OrderBy(x=>x).ToList();

			var differences = new List<int>();

			inputAsOrderedIntegers.Aggregate(0, (current, next) =>
			{
				differences.Add(next - current);
				return next;
			});

			var oneJoltDifferenceCout = differences.Where(x => x == 1).Count();
			var threeJoltDifferenceCout = differences.Where(x => x == 3).Count() + 1; // Plus the jolt difference to my device
			Console.WriteLine($"There are {oneJoltDifferenceCout} differences of 1 jolt.");
			Console.WriteLine($"There are {threeJoltDifferenceCout} differences of 3 jolt.");
			Console.WriteLine($"The product of the difference counts is {oneJoltDifferenceCout * threeJoltDifferenceCout}");
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
}
