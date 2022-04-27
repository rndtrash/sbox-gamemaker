using GM.Enums;
using GM.VM;

namespace GM.AST;

public abstract class ASTNode
{
	public abstract ASTNodeType Type { get; }

	public ASTNode()
	{
	}

	public abstract Variable Execute( Context context );
}
