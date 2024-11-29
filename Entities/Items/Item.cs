using Godot;

namespace Game.Entities.Items;

[GlobalClass]
public partial class Item : Resource
{
    [Export]
    public string Name;
	
    [Export]
    public string Description;

    [Export]
    public AtlasTexture Logo;
    
    [Export]
    public AtlasTexture Texture;

    [Export]
    public int Value;

    [Export]
    public int MaxStackSize = 1;

    private Item() : this("", "", null, null, 0, 1) {}

    public Item(string name, string description, AtlasTexture logo, AtlasTexture texture, int value, int maxStackSize)
    {
        Name = name;
        Description = description;
        Logo = logo;
        Texture = texture;
        Value = value;
        MaxStackSize = maxStackSize;
    }
}