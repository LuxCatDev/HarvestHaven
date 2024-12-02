using Game.Common.Inventory;
using Godot;
using GodotUtilities;

namespace Game.UI.InventorySlot;

[Scene]
public partial class InventorySlot : Control
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("Icon")]
	private PanelContainer _icon;

	[Node("Icon/Texture")]
	private TextureRect _texture;

	[Node("Icon/Amount")]
	private Label _amount;

	public int Index;

	public ItemStack Stack { get; private set; }

	public Inventory Inventory;

	public override Variant _GetDragData(Vector2 atPosition)
	{
		if (Stack == null) return default;

		TextureRect preview = new()
		{
			Texture = Stack.ItemType.Logo,
			Size = new(32, 32),
			ZIndex = 100
		};

		SetDragPreview(preview);

		return this;
	}
	public override bool _CanDropData(Vector2 atPosition, Variant data)
	{
		var inventorySlot = data.As<InventorySlot>();
		
		if (inventorySlot == null) return false;
		
		if (Inventory.Strict)
		{
			if (!Inventory.IsCategoryAccepted(inventorySlot.Stack.ItemType.Category)) return false;
		}
		
		return true;
	}

	public override void _DropData(Vector2 atPosition, Variant data)
	{
		InventorySlot inventorySlot = data.As<InventorySlot>();

		if (Stack != null)
		{
			if (Stack.ItemType == inventorySlot.Stack.ItemType)
			{
				Inventory.AddItem(inventorySlot.Stack);
				inventorySlot.Inventory.RemoveItemAt(inventorySlot.Index);
			}
			else
			{
				ItemStack temp = Stack;
				ItemStack temp2 = inventorySlot.Stack;

				Inventory.SetItem(temp2, Index);
				inventorySlot.Inventory.SetItem(temp, inventorySlot.Index);
			}
		}
		else
		{
			Inventory.SetItem(inventorySlot.Stack, Index);
			inventorySlot.Inventory.RemoveItemAt(inventorySlot.Index);
		}
	}

	public void SetItem(ItemStack stack)
	{
		Stack = stack;
		_texture.Texture = stack.ItemType.Logo;
		_amount.Text = stack.Amount > 1 ? stack.Amount.ToString() : "";
	}

	public void SetEmpty()
	{
		Stack = null;
		_texture.Texture = null;
		_amount.Text = "";
	}
}