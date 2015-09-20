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

