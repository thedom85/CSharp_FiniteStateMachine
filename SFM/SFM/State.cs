namespace SFM
{
	public abstract class State
	{
		public static readonly State Error = new ErrorState();
		public abstract void Handle(IContext context);
	}
}
