using Godot;

namespace Game.Entities.Items.Tools;

[GlobalClass]
public partial class ToolItem : Item
{

	[Export] public PackedScene Scene;
	
	private ToolItem() : this("", "", null, null, 0, 1, null) {}

	public ToolItem(string name, string description, AtlasTexture logo, AtlasTexture texture, int value, int maxStackSize, PackedScene scene) : base(name, description, logo, texture, value, maxStackSize)
	{
		Scene = scene;
		Category = ItemCategory.Tool;
		MaxStackSize = 1;
	}
}
