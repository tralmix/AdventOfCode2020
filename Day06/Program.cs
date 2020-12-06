using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day06
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

			var groupAnswers = new List<string>();
			var answer = string.Empty;

			foreach (var input in inputLines)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					groupAnswers.Add(answer);
					answer = string.Empty;
					continue;
				}

				answer += input;
			}

			// add last answer since no empty line trails it and for loop won't add it.
			groupAnswers.Add(answer);

			var totalYesAnswers = groupAnswers.Aggregate(0, (count, next) => count + next.ToArray().Distinct().Count());

			Console.WriteLine($"Total yes answer are {totalYesAnswers}");

			groupAnswers = new List<string>();
			var collectiveYes = new List<char>();

			var firstOfGroup = true;
			foreach (var input in inputLines)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					if(collectiveYes.Count > 0)
						groupAnswers.Add(string.Join(string.Empty, collectiveYes));
					collectiveYes = new List<char>();
					firstOfGroup = true;
					continue;
				}

				if (firstOfGroup)
				{
					collectiveYes.AddRange(input.ToArray());
					firstOfGroup = false;
				}
				else collectiveYes = collectiveYes.Where(y => input.ToArray().Contains(y)).ToList();
			}

			// add last answer since no empty line trails it and for loop won't add it.
			groupAnswers.Add(string.Join(string.Empty, collectiveYes));

			totalYesAnswers = groupAnswers.Aggregate(0, (count, next) => count + next.ToArray().Count());

			Console.WriteLine($"Total yes answer are {totalYesAnswers}");


		}
	}
}
