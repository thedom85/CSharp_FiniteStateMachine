using System.Collections.Generic;

namespace SFM
{
	public interface IContext : IDictionary<string, object>
	{
		object Command { get; }
		object Input { get; }
		object Output { set; }
		State Next { set; }
	}
}
