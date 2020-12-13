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

			var inputAsOrderedIntegers = inputLines.Select(int.Parse).OrderBy(x => x).ToList();

			// Add my device to list
			inputAsOrderedIntegers.Add(inputAsOrderedIntegers.Max() + 3);

			PartOne(inputAsOrderedIntegers);

			//var pathCount = FollowPath(0, inputAsOrderedIntegers);
			//Console.WriteLine($"There are {pathCount} possible adapter combinations");
			inputAsOrderedIntegers.Insert(0, 0);
			var allLines = GetLinePairs(inputAsOrderedIntegers);
			Console.WriteLine($"There are {allLines.Count()} graph lines");

			inputAsOrderedIntegers.Reverse();
			var nodePaths = new List<Tuple<int, int>>();
			var possibles = 0;
			foreach (var node in inputAsOrderedIntegers)
			{
				var occurancesAsStart = allLines.Count(x => x.Item1 == node);
				if (occurancesAsStart == 0)
					nodePaths.Add(new Tuple<int, int>(node, 1));
				else
				{
					var lines = allLines.Where(l => l.Item1 == node);
					possibles = 0;

					foreach (var line in lines)
						possibles += nodePaths.First(x => x.Item1 == line.Item2).Item2;

					nodePaths.Add(new Tuple<int, int>(node, possibles));
				}
			}
			possibles = nodePaths.First(n => n.Item1 == 0).Item2;
			Console.WriteLine($"There are {possibles} possibles");

		}

		private static List<Tuple<int, int>> GetLinePairs(List<int> input)
		{
			var lines = new List<Tuple<int, int>>();

			for (var index = 0; index < input.Count(); index++)
			{
				var possibles = input.Where(i => i != input[index] && i - input[index] <= 3 && i - input[index] > 0).ToList();
				foreach (var possible in possibles)
					lines.Add(new Tuple<int, int>(input[index], possible));
			}

			return lines;
		}

		private static void PartOne(List<int> inputAsOrderedIntegers)
		{
			var differences = new List<int>();

			inputAsOrderedIntegers.Aggregate(0, (current, next) =>
			{
				differences.Add(next - current);
				return next;
			});

			var oneJoltDifferenceCout = differences.Where(x => x == 1).Count();
			var threeJoltDifferenceCout = differences.Where(x => x == 3).Count();
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
