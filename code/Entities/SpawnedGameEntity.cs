using GM.Enums;
using GM.Interfaces;
using GM.VM;
using Sandbox;
using System.Collections.Generic;
namespace GM.Entities;

public class SpawnedGameEntity : AnimEntity, IHandlesEvents
{
	public IDictionary<GameEventType, Function> EventHandlers { get; set; }

	[Event( "gm.vmevent" )]
	public void VMEvent( GameEventType type, Variable[] arguments )
	{
		Host.AssertServer();

		if ( !EventHandlers.ContainsKey( type ) )
			return;

		EventHandlers[type].Run( new( VirtualMachine.The.GlobalContext, $"{EventHandlers[type].Name} ({Name}#{type})" ), arguments );
	}
}
