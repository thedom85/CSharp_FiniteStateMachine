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
