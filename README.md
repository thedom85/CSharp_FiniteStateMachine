# CSharp_FiniteStateMachine
FiniteStateMachine  is a Simple State Machine, written in C#. 
It can manage any Stateful object by defining states and transitions between these states.

Getting started
---------------

### Create Main
```C#
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


### Create State Active
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFM_Easy.States
{
	public class StateActive : SFM.State
	{

		public override void Handle(SFM.IContext context)

		{
			//Gestione parametri
			var input = (String)context.Input;
			context.Output = input;

			//Gestione Navigazione
			if ((Command)context.Command == Command.Pause) context.Next = new StatePaused();
			if ((Command)context.Command == Command.Start) context.Next = this;

		}
	}
	
}

```


### Create State Paused
```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFM_Easy.States
{
	public class StatePaused : SFM.State
	{

		public override void Handle(SFM.IContext context)
		{

			//Gestione parametri
			var input = (String)context.Input;
			context.Output = input;

			//Gestione Navigazione
			if ((Command)context.Command == Command.Start) context.Next = new StateActive();
			if ((Command)context.Command == Command.Pause) context.Next = this;


		}

	}
}


```


### Create Enum Command
```C#
namespace SFM_Easy.States
{
	public enum Command
	{
		Start,
		Pause
	}

}

```



