using Game.Entities.Items;
using Godot;

namespace Game.Common.Inventory;

[GlobalClass]
public partial class ItemStack : Resource
{
    [Export]
    public Item ItemType;

    [Export]
    public int Amount;

    public bool IsFull {
        get {
            return Amount == ItemType.MaxStackSize;
        }
    }

    public int AmountToFull {
        get {
            return ItemType.MaxStackSize - Amount;
        }
    }

    ItemStack(): this(null, 0) {}

    public ItemStack(Item item, int amount)
    {
        ItemType = item;
        Amount = amount;
    }	
}