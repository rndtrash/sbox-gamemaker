using GM.Enums;

namespace GM.VM.Variables;

public class FromFunctionVariable : Variable
{
	public override VariableType Type => VariableType.FromFunction;

	public string Function { get; set; }
	public Variable[] Arguments { get; set; }

	public FromFunctionVariable( string function, params Variable[] arguments )
	{
		Function = function;
		Arguments = arguments;
	}

	public override Variable Clone( Context context ) => Get( context ).Clone( context );
	public override Variable Get( Context context )
	{
		var f = context.GetFunction( Function );
		if ( f is null )
			throw new System.Exception( $"Unable to find function \"{Function}\" @ {string.Join( " <- ", context.GetTrace() )}" );

		return f.Run( new( context, f.Name ), Arguments );
	}
}
