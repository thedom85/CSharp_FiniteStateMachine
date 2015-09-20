using System.Collections.Generic;

namespace SFM
{
	public class Context : Dictionary<string, object>, IContext
	{
		public object Command { get; set; }
		public object Input { get; set; }
		public object Output { get; set; }
		public State Next { get; set; }
	}
}
