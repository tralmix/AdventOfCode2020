using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day08
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var inputLines = await ReadInputLines();

			var commands = inputLines.Select(BuildCommand).ToList();

			// Part One
			int accumulator = RunProgram(commands, out bool successful);

			Console.WriteLine($"Accumulator value when loop starts is {accumulator}");

			// Part Two
			var working = true;
			for (var index = 0; index < commands.Count && working; index++)
			{
				if (commands[index].Instruction == Instructions.Accumulator) continue;

				var alteredCommands = new List<Command>(commands);
				if (alteredCommands[index].Instruction == Instructions.Jump)
					alteredCommands[index] = new Command
					{
						Instruction = Instructions.NoOperation,
						Direction = commands[index].Direction,
						Value = commands[index].Value
					};
				else
					alteredCommands[index] = new Command
					{
						Instruction = Instructions.Jump,
						Direction = commands[index].Direction,
						Value = commands[index].Value
					};

				accumulator = RunProgram(alteredCommands, out successful);

				if (successful) working = false;
			}

			Console.WriteLine($"Accumulator value upon success is {accumulator}");
		}

		private static int RunProgram(List<Command> commands, out bool successful)
		{
			successful = false;
			var commandsCalled = new List<int>();

			var working = true;

			var index = 0;
			var accumulator = 0;

			while (working)
			{
				if (commandsCalled.Contains(index) || index >= commands.Count)
				{
					working = false;
					continue;
				}

				commandsCalled.Add(index);

				var command = commands[index];
				switch (command.Instruction)
				{
					case Instructions.Accumulator:
						if (command.Direction.Equals("+")) accumulator += command.Value;
						else accumulator -= command.Value;
						index++;
						break;
					case Instructions.Jump:
						if (command.Direction.Equals("+")) index += command.Value;
						else index -= command.Value;
						break;
					case Instructions.NoOperation:
						index++;
						break;
				}
			}

			if (index == commands.Count)
			{
				successful = true;
			}

			return accumulator;
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

		private static Command BuildCommand(string input)
		{
			var parts = input.Split(" ");
			var command = new Command
			{
				Instruction = parts[0],
				Direction = parts[1].Substring(0, 1),
				Value = int.Parse(parts[1].Substring(1))
			};

			return command;
		}

		class Command
		{
			public string Instruction { get; set; }
			public string Direction { get; set; }
			public int Value { get; set; }
		} 

		class Instructions
		{
			public const string Accumulator = "acc";
			public const string Jump = "jmp";
			public const string NoOperation = "nop";
		}
	}
}
