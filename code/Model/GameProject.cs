using Sandbox;
using System.Collections.Generic;

namespace GM.Model;

public partial class GameProject // TODO: somehow make this networkable, rn sbox bitches about reference types in GameEntity
{
	public string Name { get; set; }
	public long Author { get; set; } // SteamID

	public GameEntity RootEntity { get; set; } = new();

	public Dictionary<string, GameEntity> EntityPool { get; set; }

	public struct Block
	{
		public BBox BoundingBox { get; set; }
		public Color Color { get; set; }
	}
	public List<Block> StaticWorld { get; set; } = new();
	
	public struct WorldEntity
	{
		public string EntityName { get; set; }
		public Vector3 Position { get; set; }
		public Rotation Rotation { get; set; }
	}
	public List<WorldEntity> DynamicWorld { get; set; } = new();

	public void SpawnWorld()
	{
		// TODO: Generate blocks for each StaticWorld object

		RootEntity.MakeInitialInstance();

		foreach (var we in DynamicWorld)
		{
			if (!EntityPool.ContainsKey(we.EntityName))
			{
				Log.Warning( $"Unable to find \"{we.EntityName}\" object @ {we.Position}. Removing from the project..." );
				DynamicWorld.Remove( we );
				continue;
			}

			var ent = EntityPool[we.EntityName].MakeInitialInstance();
			ent.Position = we.Position;
			ent.Rotation = we.Rotation;
		}
	}
}
