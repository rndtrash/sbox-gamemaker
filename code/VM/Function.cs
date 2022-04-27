namespace GM.VM;

public abstract class Function
{
	public abstract string Name { get; internal set; }

	public abstract Variable Run( Context context, params Variable[] arguments );
}
