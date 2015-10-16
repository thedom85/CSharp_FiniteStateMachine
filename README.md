# CSharp_FiniteStateMachine
FiniteStateMachine  is a Simple State Machine, written in C#. 
It can manage any Stateful object by defining states and transitions between these states.

Finite state machines may sound like a very dry and boring topic but they reveal a lot about the power of different types of computing machine.  Every Turing machine includes a finite state machine so there is a sense in which they come first. They also turn out to be very useful in practice.

The simplest type of computing machine that is worth considering is called a ‘finite state machine’.
As it happens, the finite state machine is also a useful approach to many problems in software architecture
 
Library [Folder](https://github.com/thedom85/CSharp_FiniteStateMachine/tree/master/SFM/SFM "Folder") 

Advantages tu use my library FiniteStateMachine:

1. Define a "context" class to present a single interface to the outside world.
2. Define a State abstract base class.
3. Represent the different "states" of the state machine as derived classes of the State base class.
4. Define state-specific behavior in the appropriate State derived classes.
5. Maintain a pointer to the current "state" in the "context" class.
6. To change the state of the state machine, change the current "state" pointer.



Three examples of the use Finite State Machine :

1. Getting started (Introduction) [Folder](https://github.com/thedom85/CSharp_FiniteStateMachine/tree/master/SFM/SFM_Easy "Folder") 
2. CSV Reader (Example of reader syntax csv)[Folder](https://github.com/thedom85/CSharp_FiniteStateMachine/tree/master/SFM/SFMCSVRead/SFMCSVRead "Folder") 
3. SFM Flow Control (Example of monitoring of a flow) [Folder](https://github.com/thedom85/CSharp_FiniteStateMachine/tree/master/SFM/SFMMonitorApp "Folder") 


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

			output = machine.Command("Input_Pause", Command.Pause);
			Console.WriteLine(Command.Pause.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
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

CSV Reader
---------------

### Create Main
```C#
using System;
using SFM;
using SFMCSVRead.States;
namespace SFMCSVRead
{
	class Program
	{
		static void Main(string[] args)
		{
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
				if (output is string[]){ var campi = (string[])output;
				Console.WriteLine("{" + string.Join(", ", campi) + "}"); Console.WriteLine();}
			
			}
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}

```


### Create StateInitial
```C#
using System.Collections.Generic;
using System.Text;
using SFM;
namespace SFMCSVRead.States
{
	public class StateInitial : SFM.State
	{
		public override void Handle(IContext context)
		{
			//Console.Write(this.GetType().Name);
			var input = (char)context.Input;
			if (input == '"')
			{
				context.Next = new StateA1();
				context["actual-field"] = new StringBuilder();
				if (!context.ContainsKey("actual-line"))
				context["actual-line"] = new List<string>();
				context.Next = new StateA1();
			}
		}
	}
}
```


### Create StateA1
```C#
using System.Text;
using SFM;
namespace SFMCSVRead.States
{
	public class StateA1 : SFM.State
	{
		public override void Handle(IContext context)
		{
			//Console.Write(this.GetType().Name);
			var input = (char)context.Input;
			if (input == '"')
			{
				context.Next = new StateA2();
			}
			else
			{
				var actualField = (StringBuilder)context["actual-field"];
				actualField.Append(input);
				context.Next = this;
			}
		}
	}
}
```

### Create StateA2
```C#
using System.Collections.Generic;
using System.Text;
using SFM;
namespace SFMCSVRead.States
{
	public class StateA2 : SFM.State
	{
		public override void Handle(IContext context)
		{
			//Console.Write(this.GetType().Name);
			var input = (char)context.Input;
			var actualField = (StringBuilder)context["actual-field"];
			var actualLine = (List<string>)context["actual-line"];
			if (input == ',')
			{
				var output = actualField.ToString();
				actualLine.Add(output);
				context.Output = output;
				context.Next = new StateInitial();
			}
			else if (input == '\r')
			{
				var output = actualField.ToString();
				actualLine.Add(output);
				context.Output = output;
				context.Next = this;
			}
			else if (input == '\n')
			{
				context["actual-line"] = new List<string>();
				context.Output = actualLine.ToArray();
				context.Next = new StateInitial();
			}
		}
	}
}
```


### Create ErrorState
```C#
using SFM;
namespace SFMCSVRead.States
{
	class ErrorState : SFM.State
	{
		public override void Handle(IContext context)
		{
			context.Next = this;
		}
		public override int GetHashCode() { return GetType().GetHashCode(); }
		public override bool Equals(object obj) { return (obj is ErrorState); }
	}
}
```



SFM Flow Control 
---------------

### Create Main
```C#
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

			output = machine.Command("Input_Pause", Command.Pause);
			Console.WriteLine(Command.Pause.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);

			output = machine.Command("Input_End", Command.End);
			Console.WriteLine(Command.End.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);

			output = machine.Command("Input_Exit", Command.Exit);
			Console.WriteLine(Command.End.ToString() + "->  State: " + machine.Current);
			Console.WriteLine(output);

			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
```


### Create Enum Command
```C#
namespace SFMMonitorApp.States
{
	public enum Command
	{
		Begin,
		End,
		Pause,
		Resume,
		Exit
	}

}
```

### Create StateActive
```C#
using System;
using SFM;
namespace SFMMonitorApp.States
{
	public class StateActive : State
	{
		public override void Handle(IContext context)
		{
			//Gestione parametri
			var input = (String)context.Input;
			context.Output = input;
			//Gestione Navigazione
			if ((Command)context.Command == Command.Pause) context.Next = new StatePaused();
			if ((Command)context.Command == Command.End) context.Next = new StateInactive();
			if ((Command)context.Command == Command.Exit) context.Next = new StateTerminated();
			if ((Command)context.Command == Command.Begin) context.Next = this;
		}
	}
}
```

### Create StateInactive
```C#
using System;
using SFM;
namespace SFMMonitorApp.States
{
	public class StateInactive : State
	{

		public override void Handle(IContext context)
		{
			//Gestione Parametri
			var input = (String)context.Input;
			context.Output = input;
			//Gestione Navigazione
			if ((Command)context.Command == Command.Begin) context.Next = new StateActive();
			if ((Command)context.Command == Command.Pause) context.Next = new StatePaused();
			if ((Command)context.Command == Command.End) context.Next = this;
			if ((Command)context.Command == Command.Exit) context.Next = new StateTerminated();

		}
	}
}
```

### Create StatePaused
```C#
using System;
using SFM;

namespace SFMMonitorApp.States
{
public class StatePaused : State
{
     public override void Handle(IContext context)
     {
		 //Gestione parametri
		 var input = (String)context.Input;
		 context.Output = input;
		 //Gestione Navigazione
		 if ((Command)context.Command == Command.Begin) context.Next = new	StateActive();
		 if ((Command)context.Command == Command.End) context.Next = new	StateInactive();
		 if ((Command)context.Command == Command.Exit) context.Next = new StateTerminated();
		 if ((Command)context.Command == Command.Pause) context.Next = this;
     }
 }
}
```


### Create StateTerminated
```C#
using System;
using SFM;
namespace SFMMonitorApp.States
{
	public class StateTerminated : State
	{
			public override void Handle(IContext context)
			{
				//Gestione parametri
				var input = (String)context.Input;
				context.Output = input;
				//Gestione Navigazione
				context.Next = this;
			}

	}
}
```

### Create ErrorState
```C#
using SFM;
namespace SFMMonitorApp.States
{

	class ErrorState: State{
		public override void Handle(IContext context)
		{
		//	Console.Write(this.GetType().Name);
			context.Next = this;
		}
		public override int GetHashCode(){  return GetType().GetHashCode();    }
		public override bool Equals(object obj){ return (obj is ErrorState);   }
	}
}
```


```



