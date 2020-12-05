using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day05
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Get input
			var inputLines = new List<string>();
			using var reader = new System.IO.StreamReader("Input.txt");
			while (reader.Peek() > -1)
			{
				var lineItem = await reader.ReadLineAsync();

				inputLines.Add(lineItem);
			}

			// Part One
			var seats = new List<int>();
			var highestSeatId = 0;

			foreach(var input in inputLines)
			{
				var row = FindRow(0, 127, input.Substring(0, 7));
				var column = FindColumn(0, 7, input.Substring(7));
				var seatId = CalculateSeatId(row, column);
				if (seatId > highestSeatId) highestSeatId = seatId;

				// Part 2
				seats.Add(seatId);
			}

			Console.WriteLine($"Highest seat id is {highestSeatId}");

			seats.Sort();
			var mySeat = seats.Aggregate(seats[0], (current, next) => next == current + 1 ? next : current) + 1;

			Console.WriteLine($"My seat is {mySeat}");
		}

		static int FindRow(int start, int end, string input)
		{
			if(input.Length == 1)
			{
				if (input.Equals("F")) return start;
				return end;
			}

			var direction = input[0];
			float middle = (start + end) / 2;

			if (direction.Equals('F')) return FindRow(start, (int)Math.Floor(middle), input.Substring(1));
			
			return FindRow((int)Math.Floor(middle) + 1, end, input.Substring(1));
		}

		static int FindColumn(int start, int end, string input)
		{

			if (input.Length == 1)
			{
				if (input.Equals("L")) return start;
				return end;
			}

			var direction = input[0];
			float middle = (start + end) / 2;

			if (direction.Equals('L')) return FindColumn(start, (int)Math.Floor(middle), input.Substring(1));

			return FindColumn((int)Math.Floor(middle) + 1, end, input.Substring(1));
		}

		static int CalculateSeatId(int row, int column) => (row * 8) + column;
	}
}
