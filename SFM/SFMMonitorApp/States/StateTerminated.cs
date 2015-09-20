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


