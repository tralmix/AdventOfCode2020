using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day01
{
	public class Program
	{
		/// <summary>
		/// I know this is done horribly. Oh well.
		/// </summary>
		public static async Task Main(string[] args)
		{
			// Part One

			var input = new List<int>();
			using var reader = new System.IO.StreamReader("Input.txt");
			while (reader.Peek() > -1)
			{
				var lineItem = await reader.ReadLineAsync();

				if (int.TryParse(lineItem, out var number))
					input.Add(number);
			}

			foreach (var numerator in input)
			{
				if (input.Any(x => x + numerator == 2020))
				{
					var numerator2 = input.First(x => x + numerator == 2020);
					Console.WriteLine(numerator * numerator2);
					break;
				}
			}

			// Part Two
			var found = false;
			foreach (var numerator1 in input)
			{
				if (found) break;
				foreach (var numerator2 in input.Except(new List<int>() { numerator1 }))
				{
					if (input.Any(x => x + numerator1 + numerator2 == 2020))
					{
						var numerator3 = input.First(x => x + numerator1 + numerator2 == 2020);
						Console.WriteLine(numerator1 * numerator2 * numerator3);
						found = true;
						break;
					}
				}
			}



		}


	}
}
