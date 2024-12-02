using Godot;
using System;
using Game.Common.Inventory;
using Game.Entities.Items;
using Game.Entities.Items.Tools;

namespace Game.Entities.ToolManager;

public partial class ToolManager : Node
{

	[Signal]
	public delegate void OnToolEquipedEventHandler();
	
	[Signal]
	public delegate void OnUnEquipToolEventHandler();
	
	[Export] public Inventory ToolInventory;
	[Export] public Node2D Textures;

	private Tool _equipedTool;

	public ToolItem EquipedToolItem;
	
	public Tool EquipedTool
	{
		get => _equipedTool;
		private set
		{
			if (value != null)
			{
				_equipedTool = value;
				Textures.AddChild(value);
				EmitSignal(SignalName.OnToolEquiped);
			}
			else
			{
				_equipedTool.QueueFree();
				_equipedTool = null;
				EmitSignal(SignalName.OnUnEquipTool);
			}
		}
	}

	private void _equipTool(ItemStack item)
	{
		if (item != null)
		{
			if (item.ItemType is ToolItem)
			{
				ToolItem toolItem = (ToolItem)item.ItemType;
						
				EquipedToolItem = toolItem;

				EquipedTool = toolItem.Scene.Instantiate<Tool>();
			}
		}
		else
		{
			EquipedToolItem = null;
			EquipedTool = null;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("change_tool"))
		{
			if (EquipedTool is null)
			{
				ItemStack item = ToolInventory.Items[0];

				_equipTool(item);
			}
			else
			{
				int index = ToolInventory.Items.FindIndex(item => item.ItemType == EquipedToolItem);

				if (index != -1)
				{
					ItemStack item = ToolInventory.Items[index == ToolInventory.Items.Count ? 0 : index + 1];

					_equipTool(item);
				}
			}
		}
	}

	public void UpdateToolAnimation(string animName)
	{
		if (EquipedTool != null)
		{
			EquipedTool.UpdateAnimation(animName + "_" + GameManager.Instance.Player.AnimationDirection);
		}
	}
	
}
