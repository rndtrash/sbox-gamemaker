using GM.Enums;
using GM.VM;

namespace GM.AST;

public class CallFunctionNode : ASTNode
{
	public override ASTNodeType Type => ASTNodeType.CallFunction;

	public string Function { get; set; }
	public Variable[] Arguments { get; set; }

	public CallFunctionNode( string function, params Variable[] arguments )
	{
		Function = function;
		Arguments = arguments;
	}

	public override Variable Execute( Context context )
	{
		var f = context.GetFunction( Function );

		if ( f is null )
			throw new System.Exception( $"Unable to find function \"{Function}\" @ {string.Join( " <- ", context.GetTrace() )}" );

		return f.Run( new( context, $"{Function}" ), Arguments );
	}
}
