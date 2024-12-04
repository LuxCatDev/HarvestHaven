using System.Collections.Generic;
using Godot;
using Game.Entities.CellSelection.CellSelector;
using Game.Entities.CellSelection.Config;

namespace Game.Entities.CellSelection;

public partial class CellSelectorController : Node2D
{
	[Export] public CellSelectorConfig Config;
	[Export] private PackedScene _cellSelectorGroupScene;
	[Export] public Node2D Preview;
	
	private CellSelectorGroup _cellSelectorGroup;

	public bool Valid => _cellSelectorGroup.Valid;
	public List<CellSelector.CellSelector> Selectors => _cellSelectorGroup.Selectors;
	
	public Vector2 GroupPosition => _cellSelectorGroup.GlobalPosition;

	public override void _Process(double delta)
	{
		if (Preview != null)
		{
			Preview.GlobalPosition = _cellSelectorGroup.GlobalPosition;
		}
	}

	public void Enable()
	{
		CellSelectorGroup group = _cellSelectorGroupScene.Instantiate<CellSelectorGroup>();

		group.Config = Config;
		
		_cellSelectorGroup = group;

		if (Preview != null)
		{
			AddChild(Preview);
		}
		
		AddChild(group);
	}

	public void Disable()
	{
		if (_cellSelectorGroup != null)
		{
			_cellSelectorGroup.QueueFree();
			_cellSelectorGroup = null;
		}
	}

	public bool ExecTraits()
	{
		if (_cellSelectorGroup != null)
		{
			if (_cellSelectorGroup.Valid)
			{
				foreach (var trait in Config.Traits)
				{
					if (!trait.Validate(this))
					{
						return false;
					}

					trait.Exec(this);
				}

				return true;
			}
		}

		return false;
	}

	public void Update()
	{
		foreach (var selector in Selectors)
		{
			selector.Update();
		}
	}

	public void UpdateSize(Vector2 newSize)
	{
		_cellSelectorGroup.Config.Size = newSize;
		_cellSelectorGroup.UpdateSelectors();
	}
}
