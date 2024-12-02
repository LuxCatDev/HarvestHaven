using Godot;

namespace Game.Entities.Items.Tools.Gardening;

[GlobalClass]
public partial class GardeningToolItem : ToolItem
{

    [Export] public Vector2 DigArea;
	
    private GardeningToolItem() : this("", "", null, null, 0, 1, null, new(1,1)) {}

    public GardeningToolItem(string name, string description, AtlasTexture logo, AtlasTexture texture, int value, int maxStackSize, PackedScene scene, Vector2 digArea) : base(name, description, logo, texture, value, maxStackSize, scene)
    {
        DigArea = digArea;
    }
}