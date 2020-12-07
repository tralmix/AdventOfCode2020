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

			var bags = CreateBagObjects(inputLines);

			var bagToBeHeld = "shiny gold";

			Part_One(bags, bagToBeHeld);

			Part_Two(bags, bagToBeHeld);

		}

		private static List<Bag> CreateBagObjects(List<string> inputLines)
		{
			var bags = new List<Bag>();
			foreach (var input in inputLines)
			{
				var bag = BuildBag(input);
				bags.Add(bag);
			}

			return bags;
		}

		private static void Part_Two(List<Bag> bags, string bagToBeHeld)
		{
			var totalBagsContained = BagsContained(bags.First(b => b.Color == bagToBeHeld), bags);

			Console.WriteLine($"Total bags contained withing {bagToBeHeld}: {totalBagsContained}");
		}

		private static void Part_One(List<Bag> bags, string bagToBeHeld)
		{
			var working = true;
			var bagsThatCanHold = bags.Where(b => b.CanHold.Any(x => x.Item2.Equals(bagToBeHeld))).ToList();

			while (working)
			{
				var bagsToCheck = bags.Where(b => !bagsThatCanHold.Any(x => x.Color == b.Color));
				var additionalBagsThatCanHold = bagsToCheck.Where(b => b.CanHold.Any(x => bagsThatCanHold.Select(x => x.Color).Contains(x.Item2)));
				if (additionalBagsThatCanHold.Count() > 0)
				{
					bagsThatCanHold.AddRange(additionalBagsThatCanHold);
					continue;
				}

				// No more bags found. Stop working.
				working = false;
			}

			Console.WriteLine($"Total bags that can contain {bagToBeHeld}: {bagsThatCanHold.Count}");
		}

		private static int BagsContained(Bag bag, List<Bag> bags)
		{
			var total = bag.CanHold.Select(c=>c.Item1).Sum();
			foreach(var item in bag.CanHold)
			{
				var nextBag = bags.FirstOrDefault(b => b.Color == item.Item2);
				if(nextBag is null) continue;

				total += item.Item1 * BagsContained(nextBag, bags);
			}

			return total;
		}

		private static Bag BuildBag(string input)
		{
			var bag = new Bag();
			var bagsContainRegex = new Regex("^bag(s?[\\.,\\,]?)?$|^contain$");
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
