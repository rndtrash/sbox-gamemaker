using GM.Enums;

namespace GM.VM;

public abstract class Variable
{
	public abstract VariableType Type { get; }

	public Variable()
	{
	}

	public abstract Variable Clone( Context context );
	public abstract Variable Get( Context context );
}
