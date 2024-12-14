using Godot;
using Game.Entities.BuildingMode.Config;

namespace Game.Entities.Objects.Placeable;

[GlobalClass]
public partial class Placeable : GameObject
{
	[Export]
	public BuildingModeConfig BuildingModeConfig { get; set; }
	
	[Export]
	public int Value { get; set; }
	
	[Export]
	public AtlasTexture Icon { get; set; }
	
	[Export]
	public string Description { get; set; }
	
	public Placeable() : this("", null, ObjectCategory.Object, null, 0, null, "") { }

	public Placeable(string name, PackedScene scene, ObjectCategory category, BuildingModeConfig buildingModeConfig, int value, AtlasTexture icon, string description) : base(name, scene, category)
	{
		BuildingModeConfig = buildingModeConfig;
		Value = value;
		Icon = icon;
		Description = description;
	}

	
}
