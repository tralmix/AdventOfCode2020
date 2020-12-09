using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Day09
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var inputLines = await ReadInputLines();
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
