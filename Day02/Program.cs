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

			var failureCount = verificationRequests.Select(VerifyPassword).Count(b => b);
			Console.WriteLine($"Valid passwords: {failureCount}");
		}

		private static PasswordVerification ParsePasswordVerification(string lineItem)
		{
			var sides = lineItem.Split(':');
			var leftSideItems = sides[0].Split(' ');
			var minMaxValues = leftSideItems[0].Split('-').Select(int.Parse).ToList();

			var verifcation = new PasswordVerification
			{
				MinMax = new MinMax { Min = minMaxValues[0], Max = minMaxValues[1] },
				Character = leftSideItems[1].Trim(),
				Password = sides[1].Trim()
			};

			return verifcation;
		}

		private static bool VerifyPassword(PasswordVerification verification)
		{
			var characterCount = verification.Password.Where(c => c.ToString().Equals(verification.Character)).Count();
			return IsInRange(characterCount, verification.MinMax);
		}

		private static bool IsInRange(int value, MinMax minMax)
		{
			return value >= minMax.Min && value <= minMax.Max;
		}
	}
}
