using GM.Enums;

namespace GM.VM.Variables;

public class FromPoolVariable : Variable
{
	public override VariableType Type => VariableType.FromPool;

	public string VariableName { get; set; }

	public FromPoolVariable( string name )
	{
		VariableName = name;
	}

	public override Variable Clone( Context context ) => Get( context ).Clone( context );

	public override Variable Get( Context context )
	{
		var v = context.GetVariable( VariableName );
		if ( v is null )
			throw new System.Exception( $"Unable to find variable \"{VariableName}\" @ {string.Join( " <- ", context.GetTrace() )}" );

		return v;
	}
}
