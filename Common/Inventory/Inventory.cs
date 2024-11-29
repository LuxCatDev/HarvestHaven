using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Game.Common.Inventory;

public partial class Inventory : Node
{

    [Signal]
    public delegate void UpdatedEventHandler();

    [Export]
    public int Size;

    public List<ItemStack> Items;

    public override void _Ready()
    {
        Items = new();

        foreach (int i in Enumerable.Range(0, Size))
        {
            Items.Add(null);
        }
    }

    public bool IsFull
    {
        get
        {
            var res = Items.Where(stack => stack != null);

            return res.Count() >= Size;
        }
    }

    public int FreeSlotIndex
    {
        get
        {
            int index = Items.FindIndex((item) => item == null);

            return index;
        }
    }

    public ItemStack AddItem(ItemStack newStack)
    {
        var stacks = Items.Where(stack => stack != null && stack.ItemType == newStack.ItemType);

        var itemStacks = stacks as ItemStack[] ?? stacks.ToArray();
        if (!itemStacks.Any())
        {
            if (IsFull)
            {
                return newStack;
            }
            else
            {
                ItemStack res = CreateStacks(newStack);

                EmitSignal(SignalName.Updated);
                return res;
            }

        }

        FillStacks(newStack, itemStacks);

        if (newStack.Amount > 0)
        {
            ItemStack res = CreateStacks(newStack);

            EmitSignal(SignalName.Updated);
            return res;
        }

        EmitSignal(SignalName.Updated);

        return null;
    }

    public void RemoveItemAt(int index)
    {
        Items[index] = null;

        EmitSignal(SignalName.Updated);
    }

    public void SetItem(ItemStack stack, int index)
    {
        Items[index] = stack;

        EmitSignal(SignalName.Updated);
    }

    public ItemStack AddItemAt(ItemStack newStack, int index)
    {
        ItemStack stack = Items[index];

        if (stack.ItemType != newStack.ItemType) return newStack;

        if (stack.AmountToFull >= newStack.Amount)
        {
            Items[index].Amount += newStack.Amount;

            EmitSignal(SignalName.Updated);

            return null;
        }

        var stacks = Items.Where(stack => stack.ItemType == newStack.ItemType);

        FillStacks(newStack, stacks);

        if (newStack.Amount > 0)
        {
            ItemStack res = CreateStacks(newStack);

            EmitSignal(SignalName.Updated);
            return res;
        }

        EmitSignal(SignalName.Updated);

        return null;
    }

    public void FillStacks(ItemStack newStack, IEnumerable<ItemStack> stacks)
    {
        foreach (ItemStack stack in stacks)
        {
            if (!stack.IsFull)
            {
                if (stack.AmountToFull >= newStack.Amount)
                {
                    int index = Items.FindIndex((item) => item == stack);

                    Items[index].Amount += newStack.Amount;
                }
                else
                {
                    newStack.Amount -= stack.AmountToFull;

                    int index = Items.FindIndex((item) => item == stack);

                    Items[index].Amount = stack.ItemType.MaxStackSize;
                }
            }
        }
    }

    public ItemStack CreateStacks(ItemStack newStack)
    {
        while (newStack.Amount > newStack.ItemType.MaxStackSize)
        {
            if (IsFull)
            {
                return newStack;
            }

            Items[FreeSlotIndex] = new(newStack.ItemType, newStack.ItemType.MaxStackSize);

            newStack.Amount -= newStack.ItemType.MaxStackSize;
        }

        if (newStack.Amount != 0)
        {
            if (IsFull)
            {
                return newStack;
            }

            Items[FreeSlotIndex] = new(newStack.ItemType, newStack.Amount);
        }

        return null;
    }
}