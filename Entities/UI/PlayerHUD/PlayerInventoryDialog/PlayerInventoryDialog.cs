using System.Collections.Generic;
using System.Linq;
using Game.Common.Inventory;
using Game.UI.InventorySlot;
using Godot;
using GodotUtilities;

namespace Game.Entities.UI.PlayerHUD.PlayerInventoryDialog;

[Scene]
public partial class PlayerInventoryDialog: Control 
{
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

    [Node]
    private GridContainer _mainInventoryGrid;
    
    [Node]
    private GridContainer _toolInventoryGrid;

    private List<InventorySlot> _slots;

    private List<InventorySlot> _toolSlots;

    private Inventory _inventory;
    private Inventory _toolInventory;

    private PackedScene _slotScene = GD.Load<PackedScene>("res://Entities/UI/InventorySlot/inventory_slot.tscn");

    public override void _UnhandledInput(InputEvent @event)
    {
        if (!@event.IsActionPressed("open_inventory")) return;
        if (Visible) {
            Visible = false;
        } else {
            Visible = true;
            if (_slots == null || _slots.Count == 0) Load();
        }
    }

    public void Load()
    {
        _slots = new();
        _toolSlots = new();

        _inventory = GameManager.Instance.Player.Inventory;
        _toolInventory = GameManager.Instance.Player.ToolInventory;

        _inventory.Updated += OnInventoryUpdated;
        _toolInventory.Updated += OnToolInventoryUpdated;

        foreach(int index in Enumerable.Range(0, _inventory.Size))
        {
            InventorySlot slot = _slotScene.Instantiate<InventorySlot>();
            slot.Index = index;
            slot.Inventory = _inventory;

            _mainInventoryGrid.AddChild(slot);

            _slots.Add(slot);
            
                if (_inventory.Items[slot.Index] != null)
                {
                    slot.SetItem(_inventory.Items[slot.Index]);
                }
                else
                {
                    slot.SetEmpty();
                }
        }
        
        foreach(int index in Enumerable.Range(0, _toolInventory.Size))
        {
            InventorySlot slot = _slotScene.Instantiate<InventorySlot>();
            slot.Index = index;
            slot.Inventory = _toolInventory;

            _toolInventoryGrid.AddChild(slot);

            _toolSlots.Add(slot);
            
            if (_toolInventory.Items[slot.Index] != null)
            {
                slot.SetItem(_toolInventory.Items[slot.Index]);
            }
            else
            {
                slot.SetEmpty();
            }
        }
    }

    public void OnInventoryUpdated()
    {
        foreach(InventorySlot slot in _slots)
        {
            if (_inventory.Items[slot.Index] != null)
            {
                slot.SetItem(_inventory.Items[slot.Index]);
            } else {
                slot.SetEmpty();
            }
        }
    }
    
    public void OnToolInventoryUpdated()
    {
        foreach(InventorySlot slot in _toolSlots)
        {
            if (_toolInventory.Items[slot.Index] != null)
            {
                slot.SetItem(_toolInventory.Items[slot.Index]);
            } else {
                slot.SetEmpty();
            }
        }
    }
}