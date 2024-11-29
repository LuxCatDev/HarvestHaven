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

    private List<InventorySlot> _slots;

    private Inventory _inventory;

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

        _inventory = GameManager.Instance.Player.Inventory;

        _inventory.Updated += OnInventoryUpdated;

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
}