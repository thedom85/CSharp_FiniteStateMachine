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
