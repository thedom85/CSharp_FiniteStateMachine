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
