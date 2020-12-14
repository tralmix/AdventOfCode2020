using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day11
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var inputLines = await ReadInputLines();

			var seatMap = inputLines.Select(i => i.Select(x => $"{x}").ToList()).ToList();

			UpdateSeatsUntilEveryoneSettles(seatMap);

			var occupiedCount = seatMap.Aggregate(0, (current, next) => current + next.Where(x => x.Equals(SeatStates.Taken)).Count());

			Console.WriteLine($"There are {occupiedCount} seats occupied");
		}

		private static void UpdateSeatsUntilEveryoneSettles(List<List<string>> seatMap)
		{
			var seatsToUpdate = new List<Tuple<int, int>>();

			do
			{
				seatsToUpdate.Clear();
				// Find seats to update
				for (var i = 0; i < seatMap.Count(); i++)
				{
					var row = seatMap[i];
					for (var j = 0; j < row.Count(); j++)
					{
						var seat = seatMap[i][j];
						if (seat.Equals(SeatStates.Floor))
							continue; // Floor spots never change

						if (seat.Equals(SeatStates.Empty))
						{
							if (CountOpposingNeighbors(seatMap, i, j, SeatStates.Taken) == 0)
								seatsToUpdate.Add(new Tuple<int, int>(i, j));
						}

						if (seat.Equals(SeatStates.Taken))
						{
							if (CountOpposingNeighbors(seatMap, i, j, SeatStates.Taken) >= 4)
								seatsToUpdate.Add(new Tuple<int, int>(i, j));
						}
					}
				}

				// Update seats
				foreach (var seat in seatsToUpdate)
				{
					if (seatMap[seat.Item1][seat.Item2].Equals(SeatStates.Empty))
						seatMap[seat.Item1][seat.Item2] = SeatStates.Taken;
					else
						seatMap[seat.Item1][seat.Item2] = SeatStates.Empty;
				}
			} while (seatsToUpdate.Any());
		}

		private static int CountOpposingNeighbors(List<List<string>> seatMap, int i, int j, string seatToFind)
		{
			var opposingNeighbors = 0;
			var row = seatMap[i];
			if (i > 0)
			{
				if (j > 0)
					if (seatMap[i - 1][j - 1].Equals(seatToFind)) opposingNeighbors++;
				if (seatMap[i - 1][j].Equals(seatToFind)) opposingNeighbors++;
				if(j< row.Count() - 1)
					if (seatMap[i - 1][j + 1].Equals(seatToFind)) opposingNeighbors++;
			}

			if (j > 0)
				if (seatMap[i][j - 1].Equals(seatToFind)) opposingNeighbors++;

			if (j < row.Count() - 1)
				if (seatMap[i][j + 1].Equals(seatToFind)) opposingNeighbors++;

			if (i <seatMap.Count() - 1)
			{
				if (j > 0)
					if (seatMap[i + 1][j - 1].Equals(seatToFind)) opposingNeighbors++;
				if (seatMap[i + 1][j].Equals(seatToFind)) opposingNeighbors++;
				if (j < row.Count() - 1)
					if (seatMap[i + 1][j + 1].Equals(seatToFind)) opposingNeighbors++;
			}

			return opposingNeighbors;
		}

		static class SeatStates
		{
			public const string Floor = ".";
			public const string Empty = "L";
			public const string Taken = "#";
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
