using GM.AST;
using GM.Enums;
using GM.Model;
using GM.UI;
using GM.VM;
using GM.VM.Functions;
using GM.VM.Variables;
using Sandbox;
using System.Linq;

namespace GM;

public partial class GMMain : Game
{
	public static GMMain The { get; internal set; }

	public enum GMState
	{
		Invalid,
		ProjectSelection,
		Editor,
		InGame
	}
	[Net, Change] public GMState State { get; internal set; } = GMState.Invalid;

	public GameProject CurrentProject { get; set; } = null;

	public GMMain()
	{
		The = this;

		if ( IsServer )
		{
			_ = new VirtualMachine();
			_ = new UIEntity();

			State = GMState.ProjectSelection;
		}
	}

	/// <summary>
	/// A client has joined the server. Make them a pawn to play with
	/// </summary>
	public override void ClientJoined( Client client )
	{
		base.ClientJoined( client );
	}

	[AdminCmd( "gm_new" )]
	public static void NewProject( string name )
	{
		if ( The.CurrentProject is not null )
			StopProject();

		The.CurrentProject = new()
		{
			Name = name,
			Author = ConsoleSystem.Caller.PlayerId
		};

		The.CurrentProject.RootEntity.EventHandlers.Add(
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

		The.State = GMState.Editor;
	}

	[AdminCmd( "gm_run" )]
	public static void RunProject()
	{
		if ( The.CurrentProject is null )
			return;

		StopProject(); // just in case

		The.CurrentProject.SpawnWorld();
		Event.Run( "gm.run" );
		Event.Run( "gm.vmevent", GameEventType.Start, System.Array.Empty<Variable>() );
	}

	[AdminCmd( "gm_stop" )]
	public static void StopProject()
	{
		if ( The.CurrentProject is null )
			return;

		VirtualMachine.The.Reset();

		foreach ( var c in The.Children.ToArray() )
		{
			c.Delete();
		}

		Event.Run( "gm.stop" );
	}

	[AdminCmd( "gm_debug_context" )]
	public static void PrintGlobalContext()
	{
		if ( VirtualMachine.The is null )
			return;

		Log.Info( $"Function pool:\n{string.Join( '\n', VirtualMachine.The.GlobalContext.FunctionPool.Keys )}" );
		Log.Info( $"Variable pool:\n{string.Join( '\n', VirtualMachine.The.GlobalContext.VariablePool.Keys )}" );
	}

	public void OnStateChanged()
	{
		Event.Run( "gm.state", State );
	}
}
