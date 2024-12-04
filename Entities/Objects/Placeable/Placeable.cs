using Godot;
using Game.Entities.BuildingMode.Config;

namespace Game.Entities.Objects.Placeable;

[GlobalClass]
public partial class Placeable : GameObject
{
	[Export]
	public BuildingModeConfig BuildingModeConfig { get; set; }
	
	public Placeable() : this("", null, ObjectCategory.Object, null) { }

	public Placeable(string name, PackedScene scene, ObjectCategory category, BuildingModeConfig buildingModeConfig) : base(name, scene, category)
	{
		BuildingModeConfig = buildingModeConfig;
	}

	
}
