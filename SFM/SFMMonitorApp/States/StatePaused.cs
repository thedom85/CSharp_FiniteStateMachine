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
