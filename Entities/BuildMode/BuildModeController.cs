using System.Drawing;
using Godot;
using Game.Entities.CellSelection;
using Game.Entities.ObjectInventory;
using Game.Entities.Objects.Placeable;
using Game.UI.BuildMode;
using Game.Utilities.GameMode;
using GodotUtilities;

namespace Game.Entities.BuildMode;

[Scene]
public partial class BuildModeController : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Signal]
	public delegate void OnSelectedObjectChangedEventHandler();
	
	[Export] public Placeable Placeable;
	[Export] private CellSelectorController _cellSelectorController;

	[Export] public ObjectInventory.ObjectInventory Inventory;
	
	[Node("BuildModeUi")]
	private BuildModeUi _buildModeUi;
	
	private PlaceableObject _actualObject;

	public ObjectItem ItemSelected;

	public Vector2 Orientation = Vector2.Down;

	public bool Active;

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("open_build_mode"))
		{
			if (Visible)
			{
				_Close();
			}
			else
			{
				_Open();
			}
		}
		
		if (!Active) return;
		
		if (@event.IsActionPressed("rotate"))
		{
			Rotate();
		}
	}

	public override void _Ready()
	{
	}

	private void _Open()
	{
		GameManager.Instance.ChangeMode(GameMode.Build);
		Show();
		LoadInventory();
		_buildModeUi.Show();
	}

	public void _Close()
	{
		Hide();
		_buildModeUi.Hide();
		Disable();
		GameManager.Instance.ChangeMode(GameMode.Normal);
	}

	public void LoadInventory()
	{
		_buildModeUi.SetItems(Inventory.Items);
	}

	public void Enable()
	{
		Active = true;
		_cellSelectorController.Config = Placeable.BuildingModeConfig.CellSelectorConfig;
		_actualObject = Placeable.Scene.Instantiate<PlaceableObject>();
		_actualObject.Preview = true;
		_cellSelectorController.Preview = _actualObject;
		_cellSelectorController.Enable();
	}

	public void Disable()
	{
		Active = false;
		_cellSelectorController.Config = null;
		if (_actualObject != null)
		{
			_actualObject.QueueFree();
		}
		_actualObject = null;
		_cellSelectorController.Preview = null;
		_cellSelectorController.Disable();
	}

	public void Place()
	{
		_cellSelectorController.Config = Placeable.BuildingModeConfig.CellSelectorConfig;
		_cellSelectorController.ExecTraits();
	}

	public void Rotate()
	{
		if (_actualObject != null)
		{
			var index = _actualObject.Orientations.FindIndex(i => i == _actualObject.Orientation);

			if (index == -1) return;

			Vector2 size = _cellSelectorController.Config.Size;

			Vector2 newSize = size.RotatedDegrees(90).Snapped(Vector2.One);

			if (newSize.X < 0)
			{
				newSize.X *= -1;
			}
			
			if (newSize.Y < 0)
			{
				newSize.Y *= -1;
			}
			
			_cellSelectorController.UpdateSize(newSize);
			
			_actualObject.Orientation =
				_actualObject.Orientations[index == _actualObject.Orientations.Count - 1 ? 0 : index + 1];
		}
	}

	private void _OnObjectSelected(ObjectItem item)
	{
		Placeable = item.Object;
		ItemSelected = item;
		Enable();

		EmitSignal(SignalName.OnSelectedObjectChanged);
	}
}
