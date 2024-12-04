using System.Drawing;
using Godot;
using Game.Entities.BuildingMode.Config;
using Game.Entities.CellSelection;
using Game.Entities.Objects.Placeable;
using GodotUtilities;

namespace Game.Entities.BuildingMode;

public partial class BuildingModeController : Node2D
{
	[Export] public Placeable Placeable;
	[Export] private CellSelectorController _cellSelectorController;
	
	private PlaceableObject _actualObject;

	public Vector2 Orientation = Vector2.Down;

	public bool Active;

	public override void _Input(InputEvent @event)
	{
		if (!Active) return;
		
		if (@event.IsActionPressed("rotate"))
		{
			Rotate();
		}
	}

	public override void _Ready()
	{
		Enable();
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
}
