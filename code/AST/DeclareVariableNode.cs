using GM.Enums;
using GM.VM;
using GM.VM.Variables;
using System;

namespace GM.AST;

public class DeclareVariableNode : ASTNode
{
	public override ASTNodeType Type => ASTNodeType.DeclareVariable;

	public string Name { get; set; }
	public Variable Variable { get; set; }

	public DeclareVariableNode( string name, Variable variable )
	{
		Name = name;
		Variable = variable;
	}

	public override Variable Execute( Context context )
	{
		if ( context.VariablePool.ContainsKey( Name ) )
			throw new Exception( $"Variable \"{Name}\" has already been declared! @ {string.Join( " <- ", context.GetTrace() )}" );

		if ( Variable.Type == VariableType.FromPool )
			Variable = (Variable as FromPoolVariable).Get( context );
		context.VariablePool.Add( Name, Variable );

		return null;
	}
}
