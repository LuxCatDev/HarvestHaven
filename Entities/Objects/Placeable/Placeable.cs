using Godot;
using Game.Entities.BuildMode.Config;

namespace Game.Entities.Objects.Placeable;

[GlobalClass]
public partial class Placeable : GameObject
{
	[Export]
	public BuildModeConfig BuildModeConfig { get; set; }
	
	[Export]
	public int Value { get; set; }
	
	[Export]
	public AtlasTexture Icon { get; set; }
	
	[Export]
	public string Description { get; set; }
	
	public Placeable() : this("", null, ObjectCategory.Object, null, 0, null, "") { }

	public Placeable(string name, PackedScene scene, ObjectCategory category, BuildModeConfig buildModeConfig, int value, AtlasTexture icon, string description) : base(name, scene, category)
	{
		BuildModeConfig = buildModeConfig;
		Value = value;
		Icon = icon;
		Description = description;
	}

	
}
