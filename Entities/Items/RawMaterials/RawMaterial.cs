using Godot;

namespace Game.Entities.Items.RawMaterials;

[GlobalClass]
public partial class RawMaterial : Item
{
    private RawMaterial() : this("", "", null, null, 0, 1) {}

    public RawMaterial(string name, string description, AtlasTexture logo, AtlasTexture texture, int value, int maxStackSize) : base(name, description, logo, texture, value, maxStackSize)
    {
    }
}