using GM.Entities;
using GM.Enums;
using GM.Interfaces;
using GM.VM;
using System;
using System.Collections.Generic;

namespace GM.Model;

public class GameEntity : IHandlesEvents
{
	public IDictionary<GameEventType, Function> EventHandlers { get; set; } = new Dictionary<GameEventType, Function>();

	public SpawnedGameEntity MakeInitialInstance()
	{
		SpawnedGameEntity ent = new();

		ent.EventHandlers = EventHandlers;

		ent.Parent = GMMain.The;

		return ent;
	}

	public SpawnedGameEntity MakeInstance()
	{
		var ent = MakeInitialInstance();

		ent.VMEvent( GameEventType.Start, Array.Empty<Variable>() );

		return ent;
	}
}
