using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
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

			var bags = new List<Bag>();
			foreach (var input in inputLines)
			{
				var bag = BuildBag(input);
				bags.Add(bag);
			}
		}

		private static Bag BuildBag(string input)
		{
			var bag = new Bag();
			var bagsContainRegex = new Regex("^bag(s[\x2E,\x2C]?)?$|^contain$");
			var numericRegex = new Regex("^[0-9]+$");
			var words = input.Split(" ");
			var color = string.Empty;
			var step = Steps.BagColor;
			var itemCount = 0;
			foreach (var word in words.Where(w => !bagsContainRegex.IsMatch(w)))
			{
				if (string.IsNullOrEmpty(color))
				{
					color = word;
					continue;
				}

				switch (step)
				{
					case Steps.BagColor:
						if (numericRegex.IsMatch(word))
						{
							bag.Color = color;
							color = string.Empty;
							step = Steps.ItemCount;
							goto case Steps.ItemCount;
						}
						color += $" {word}";
						break;
					case Steps.ItemCount:
						itemCount = int.Parse(word);
						step = Steps.ItemColor;
						break;
					case Steps.ItemColor:
						if (numericRegex.IsMatch(word))
						{
							bag.CanHold.Add(new Tuple<int, string>(itemCount, color));
							color = string.Empty;
							step = Steps.ItemCount;
							goto case Steps.ItemCount;
						}
						color += $" {word}";
						break;
				}
			}

			// add last item that was found in above loop
			bag.CanHold.Add(new Tuple<int, string>(itemCount, color));

			return bag;
		}

		class Bag
		{
			public Bag()
			{
				CanHold = new List<Tuple<int, string>>();
			}
			public string Color { get; set; }
			public List<Tuple<int, string>> CanHold { get; set; }

		}

		enum Steps
		{
			BagColor,
			ItemCount,
			ItemColor
		}
	}
}
