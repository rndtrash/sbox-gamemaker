using GM.Enums;
using GM.VM;
using System.Collections.Generic;

namespace GM.Interfaces;

public interface IHandlesEvents
{
	public IDictionary<GameEventType, Function> EventHandlers { get; set; }
}
