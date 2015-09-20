using System;
using SFM;
using SFMMonitorApp.States;

namespace SFMMonitorApp
{
	class Program
	{
		static void Main(string[] args)
		{


			var machine = new Machine(new StateInactive());
			var output = machine.Command("Input_Begin", Command.Begin);
			Console.WriteLine(Command.Begin.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			output = machine.Command("Input_Pause", Command.Pause);
			Console.WriteLine(Command.Pause.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			output = machine.Command("Input_End", Command.End);
			Console.WriteLine(Command.End.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			output = machine.Command("Input_Exit", Command.Exit);
			Console.WriteLine(Command.End.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			Console.WriteLine("");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
