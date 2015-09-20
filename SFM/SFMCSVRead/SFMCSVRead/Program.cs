using System;
using SFM;
using SFMCSVRead.States;

namespace SFMCSVRead
{
	class Program
	{
		static void Main(string[] args)
		{


			//var machine = new Machine(new StateInactive());

			var csv = "\"ciao\",\"dvdsovnso\"\r\n";

			var machine = new Machine(new StateInitial());

			foreach (var c in csv)
			{
				//Console.WriteLine("");
				//Console.Write(c+" -> ");
				var output = machine.Process(c);
				if (machine.Current == State.Error){Console.WriteLine("ERRORE");Console.ReadLine();return;}
				if (output == null) continue;
				if (output is string){Console.WriteLine("{" + output + "}");}
				if (output is string[]){ var campi = (string[])output; Console.WriteLine("{" + string.Join(", ", campi) + "}"); Console.WriteLine();}
			
			}



			//Console.ReadLine();

			Console.WriteLine("");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}

			//--------------------------------------------------------

	
	}
}
