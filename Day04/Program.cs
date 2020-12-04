using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04
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

			// Build passports
			var passports = new List<Passport>();
			var passport = new Passport();
			foreach(var line in inputLines)
			{
				if(string.IsNullOrWhiteSpace(line))
				{
					passports.Add(passport);
					passport = new Passport();
					continue;
				}

				var fields = line.Split(" ").ToList();
				foreach(var field in fields)
				{
					var parts = field.Split(":");
					switch(parts[0])
					{
						case PassportFields.BirthYear:
							passport.BirthYear = parts[1];
							break;
						case PassportFields.IssueYear:
							passport.IssueYear = parts[1];
							break;
						case PassportFields.ExpirationYear:
							passport.ExpirationYear = parts[1];
							break;
						case PassportFields.Height:
							passport.Height = parts[1];
							break;
						case PassportFields.HairColor:
							passport.HairColor = parts[1];
							break;
						case PassportFields.EyeColor:
							passport.EyeColor = parts[1];
							break;
						case PassportFields.PassportId:
							passport.PassportId = parts[1];
							break;
						case PassportFields.CountryId:
							passport.CountryId = parts[1];
							break;
					}
				}
			}
			passports.Add(passport);

			var validPassports = passports.Where(p => p.IsValid_Part1()).Count();
			Console.WriteLine($"Valid passports: {validPassports}");

			// Part 2

			validPassports = passports.Where(p => p.IsValid_Part1() && p.IsValid_Part2()).Count();
			Console.WriteLine($"Valid passports: {validPassports}");
		}

		class Passport
		{
			public string BirthYear { get; set; }
			public string IssueYear { get; set; }
			public string ExpirationYear { get; set; }
			public string Height { get; set; }
			public string HairColor { get; set; }
			public string EyeColor { get; set; }
			public string PassportId { get; set; }
			public string CountryId { get; set; }

			public bool IsValid_Part1()
			{
				if (string.IsNullOrWhiteSpace(BirthYear)
					|| string.IsNullOrWhiteSpace(IssueYear)
					|| string.IsNullOrWhiteSpace(ExpirationYear)
					|| string.IsNullOrWhiteSpace(Height)
					|| string.IsNullOrWhiteSpace(HairColor)
					|| string.IsNullOrWhiteSpace(EyeColor)
					|| string.IsNullOrWhiteSpace(PassportId))
					return false;

				return true;
			}
			
			public bool IsValid_Part2()
			{
				// Birth Year
				if (!new Regex("^(19[2-9][0-9])|(200[0-2])$").IsMatch(BirthYear))
					return false;

				// Issue Year
				if (!new Regex("^20(1[0-9]|20)$").IsMatch(IssueYear))
					return false;

				// Expiration Year
				if (!new Regex("^20(2[0-9]|30)$").IsMatch(ExpirationYear))
					return false;

				// Height
				if (!new Regex("^1([5-8][0-9]|9[0-3])cm$|^(59|6[0-9]|7[0-6])in$").IsMatch(Height))
					return false;

				// Hair Color
				if (!new Regex("^#[0-9a-f]{6}$").IsMatch(HairColor)) 
					return false;

				// Eye Color
				if (!new Regex("^amb$|^blu$|^brn$|^gry$|^grn$|^hzl$|^oth$").IsMatch(EyeColor)) 
					return false;

				// Passport Id
				if (!new Regex("^[0-9]{9}$").IsMatch(PassportId)) 
					return false;

				return true;
			}
		}

		static class PassportFields
		{
			public const string BirthYear = "byr";
			public const string IssueYear = "iyr";
			public const string ExpirationYear = "eyr";
			public const string Height = "hgt";
			public const string HairColor = "hcl";
			public const string EyeColor = "ecl";
			public const string PassportId = "pid";
			public const string CountryId = "cid";
		}
	}
}
