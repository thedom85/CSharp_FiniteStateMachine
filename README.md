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
				if (output is string[]){ var campi = (string[])output; Console.WriteLine("{" + string.Join(", ", campi) + "}"); Console.WriteLine();}
			
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




