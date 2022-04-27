using GM.AST;
using GM.Enums;
using GM.VM;
using GM.VM.Functions;
using GM.VM.Variables;
using Sandbox;
using Sandbox.UI;
using System.Linq;

namespace GM.UI;

public class EditorPanel : Panel
{
	public Function CurrentFunction { get; protected set; }

	public EditorPanel()
	{
		// i hate the antichrist
		GMMain.The.CurrentProject = new()
		{
			Name = "rodent",
			Author = Local.Client.PlayerId
		};

		GMMain.The.CurrentProject.RootEntity.EventHandlers.Add(
			GameEventType.Start,
			new UserDefinedFunction(
				new DeclareVariableNode(
					"test",
					new StringVariable( "hello world!" )
				),
				new CallFunctionNode(
					"Log",
					new FromPoolVariable( "test" )
				)
			)
		);

		CurrentFunction = GMMain.The.CurrentProject.RootEntity.EventHandlers[GameEventType.Start];

		RenderFunction(CurrentFunction);
	}

	protected void RenderFunction(Function function)
	{
		if ( function is not UserDefinedFunction udf )
			return;

		foreach (var c in Children.ToArray())
		{
			c.Delete( true );
		}

		foreach (var i in udf.Instructions)
		{
			switch (i.Type)
			{
				case ASTNodeType.DeclareVariable:
					{
						var node = AddChild<Panel>( "ast-node" );
						var n = i as DeclareVariableNode;
						var p = node.AddChild<Panel>( "single ast-declare-variable" );
						p.AddChild<Label>( "input" ).Text = n.Name;
						p.AddChild<Label>().Text = " = ";
						p.AddChild<Label>( "input" ).Text = (n.Variable as StringVariable).Value;
					}
					break;
				case ASTNodeType.CallFunction:
					{
						var node = AddChild<Panel>( "ast-node" );
						var n = i as CallFunctionNode;
						var p = node.AddChild<Panel>( "single ast-call-function" );
						p.AddChild<Label>().Text = "Call ";
						p.AddChild<Label>( "input" ).Text = n.Function;
						p.AddChild<Label>().Text = " with ";
						p.AddChild<Label>( "input" ).Text = (n.Arguments[0] as FromPoolVariable).VariableName;
					}
					break;
				default:
					throw new System.Exception( $"Unhandled node type \"{i.Type}\"" );
			}
		}
	}
}
