# CSharp_FiniteStateMachine
FiniteStateMachine  is a Simple State Machine, written in C#. 
It can manage any Stateful object by defining states and transitions between these states.

Getting started
---------------


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


### Create State StatePaused
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
