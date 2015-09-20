using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFM_Easy.States;

namespace SFM_Easy
{
	class Program
	{
		static void Main(string[] args)
		{
			var machine = new SFM.Machine(new StatePaused());
			var output = machine.Command("Input_Start", Command.Start);
			Console.WriteLine(Command.Start.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			output = machine.Command("Input_Pause", Command.Pause);
			Console.WriteLine(Command.Pause.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);
			Console.WriteLine("-------------------------------------------------");

			Console.WriteLine("");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
