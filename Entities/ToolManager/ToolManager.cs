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

	[Signal]
	public delegate void OnToolUsedEventHandler();
	
	[Signal]
	public delegate void OnStopUsingEventHandler();
	
	[Export] public Inventory ToolInventory;
	[Export] public Node2D Textures;

	private Tool _equipedTool;

	public ToolItem EquipedToolItem;

	public bool Pause = false;
	
	public Tool EquipedTool
	{
		get => _equipedTool;
		private set
		{
			if (value != null)
			{
				_equipedTool = value;
				value.OnUsed += () => EmitSignal(SignalName.OnToolUsed);
				value.OnStopUsing += () => EmitSignal(SignalName.OnStopUsing);
				Textures.AddChild(value);
				value.Equip();
				EmitSignal(SignalName.OnToolEquiped);
			}
			else
			{
				if (_equipedTool != null) _equipedTool.QueueFree();
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
			if (EquipedTool != null)
			{
				EquipedTool.UnEquip();
			}
			EquipedToolItem = null;
			EquipedTool = null;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (Pause) return;
		
		if (@event.IsActionPressed("change_tool"))
		{
			if (EquipedTool is null)
			{
				ItemStack item = ToolInventory.Items[ToolInventory.Items.FindIndex(item => item != null)];
				

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

		if (@event.IsActionPressed("primary_action"))
		{
			if (EquipedTool != null)
			{
				EquipedTool.Use();
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
