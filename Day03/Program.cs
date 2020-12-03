using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day03
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Part One

			var inputLines = new List<string>();
			using var reader = new System.IO.StreamReader("Input.txt");
			while (reader.Peek() > -1)
			{
				var lineItem = await reader.ReadLineAsync();

				inputLines.Add(lineItem);
			}

			var treeLines = GetExpandedTreeLines(inputLines, 3);

			int treeCount = 0, rightIndex = 0, downIndex = 0;

			do
			{
				if (treeLines[downIndex][rightIndex].Equals('#'))
					treeCount++;

				rightIndex += 3;
				downIndex += 1;

			} while (downIndex < treeLines.Count());

			Console.WriteLine($"Tree count is {treeCount}");

			// Part Two

			var slopes = new List<Slope> {
				new Slope { down = 1, right=1 },
				new Slope { down = 1, right=3 },
				new Slope { down = 1, right=5 },
				new Slope { down = 1, right=7 },
				new Slope { down = 2, right=1 },
			};

			var treeCounts = new List<long>();

			foreach(var slope in slopes)
			{
				treeLines = GetExpandedTreeLines(inputLines, slope.right);
				treeCount = 0;
				rightIndex = 0;
				downIndex = 0;

				do
				{
					if (treeLines[downIndex][rightIndex].Equals('#'))
						treeCount++;

					rightIndex += slope.right;
					downIndex += slope.down;

				} while (downIndex < treeLines.Count());

				treeCounts.Add(treeCount);

				Console.WriteLine($"Tree count is {treeCount}");
			}

			var product = treeCounts.Aggregate((a, b) => a * b);

			Console.WriteLine($"Product of tree lines is {product}");
		}

		private static List<List<char>> GetExpandedTreeLines(List<string> inputLines, int slope)
		{
			var treeLines = inputLines.Select(x => x.ToArray().ToList()).ToList();
			while (treeLines[0].Count() < slope * treeLines.Count())
			{
				foreach (var line in treeLines)
					line.AddRange(line);
			}

			return treeLines;
		}
		struct Slope
		{
			public int down;
			public int right;
		}
	}
}
