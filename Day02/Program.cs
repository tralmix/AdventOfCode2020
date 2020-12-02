using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day02
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Part One

			var verificationRequests = new List<PasswordVerification>();
			using var reader = new System.IO.StreamReader("Input.txt");
			while (reader.Peek() > -1)
			{
				var lineItem = await reader.ReadLineAsync();

				verificationRequests.Add(ParsePasswordVerification(lineItem));
			}

			var validCount = verificationRequests.Select(VerifyPassword_Part1).Count(b => b);
			Console.WriteLine($"Valid passwords: {validCount}");

		}

		private static PasswordVerification ParsePasswordVerification(string lineItem)
		{
			var sides = lineItem.Split(':');
			var leftSideItems = sides[0].Split(' ');
			var minMaxValues = leftSideItems[0].Split('-').Select(int.Parse).ToList();

			var verifcation = new PasswordVerification
			{
				RuleValues = new Tuple<int, int>(minMaxValues[0], minMaxValues[1]),
				Character = leftSideItems[1].Trim(),
				Password = sides[1].Trim()
			};

			return verifcation;
		}

		private static bool VerifyPassword_Part1(PasswordVerification verification)
		{
			var characterCount = verification.Password.Where(c => c.ToString().Equals(verification.Character)).Count();
			return IsInRange(characterCount, verification.RuleValues);
		}

		private static bool IsInRange(int value, Tuple<int, int> minMax)
		{
			return value >= minMax.Item1 && value <= minMax.Item2;
		}
	}
}
