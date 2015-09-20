namespace SFM
{
	public class Machine
	{

	
		public Context Context;
		public State Initial { get; private set; }
		public State Current { get; private set; }
		//public void Reset(){}
		// public object Process(object input){  return null;  }


		public Machine(State initial) { Initial = initial; Reset(); }
		public void Reset() { Current = Initial; Context = new Context(); }
		public object Process(object input)
		{
			Context.Input = input;
			Context.Next = null;
			Context.Output = null;
			Current.Handle(Context);
			Current = Context.Next ?? State.Error;
			//Current = Context.Next ?? new StateA1();

			return Context.Output;
		}
		public object Command(object input,object command)
		{
			Context.Command = command;
			Context.Input = input;
			Context.Next = null;
			Context.Output = null;
			Current.Handle(Context);
			Current = Context.Next ?? State.Error;
			//Current = Context.Next ?? new StateA1();

			return Context.Output;
		}


	}
}
