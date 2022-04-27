using GM.AST;
using System.Collections.Generic;

namespace GM.VM.Functions;

public class UserDefinedFunction : Function
{
	public List<ASTNode> Instructions { get; set; }
	public override string Name { get; internal set; }

	public UserDefinedFunction(params ASTNode[] instructions) : this("Anonymous", instructions)
	{
	}

	public UserDefinedFunction( string name, params ASTNode[] instructions )
	{
		Name = name;
		Instructions = new( instructions );
	}

	public override Variable Run( Context context, params Variable[] arguments )
	{
		foreach (var instruction in Instructions)
		{
			Log.Info( $"Executing {instruction.Type}..." );
			instruction.Execute( context );

			// TOOD: if (instruction is ReturnNode ret) { return ret.Value; }
		}

		return null;
	}
}
