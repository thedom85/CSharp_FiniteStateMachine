﻿using SFM;

namespace SFMCSVRead.States
{
	class ErrorState : SFM.State
	{

		public override void Handle(IContext context)
		{
			//	Console.Write(this.GetType().Name);
			context.Next = this;

		}
		public override int GetHashCode() { return GetType().GetHashCode(); }
		public override bool Equals(object obj) { return (obj is ErrorState); }

	}
}
