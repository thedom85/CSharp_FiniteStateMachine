﻿using System;
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
