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

			var treeLines = GetExpandedTreeLines(inputLines);

			int treeCount = 0, rightIndex = 0, downIndex = 0;

			do
			{
				if (treeLines[downIndex][rightIndex].Equals('#'))
					treeCount++;

				rightIndex += 3;
				downIndex += 1;

			} while (downIndex < treeLines.Count());

			Console.WriteLine($"Tree count is {treeCount}");
		}

		private static List<List<char>> GetExpandedTreeLines(List<string> inputLines)
		{
			var treeLines = inputLines.Select(x => x.ToArray().ToList()).ToList();
			while (treeLines[0].Count() < 3* treeLines.Count())
			{
				foreach (var line in treeLines)
					line.AddRange(line);
			}

			return treeLines;
		}
	}
}
