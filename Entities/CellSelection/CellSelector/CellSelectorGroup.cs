using System.Collections.Generic;
using Godot;
using System.Linq;
using Game.Entities.CellSelection.Config;
using GodotUtilities;

namespace Game.Entities.CellSelection.CellSelector;

[Scene]
public partial class CellSelectorGroup : Node2D
{
	
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Export] private PackedScene _cellSelectorScene;

	private List<CellSelector> _selectors;

	[Node("Selectors")] private Node2D _selectorsWrapper;
	
	public CellSelectorConfig Config;

	public List<CellSelector> Selectors
	{
		get => _selectors;
	}

	public bool Valid
	{
		get
		{
			foreach (var selector in _selectors)
			{
				if (!selector.State)
				{
					return false;
				}
			}

			return true;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		UpdateSelectors();
	}

	public void UpdateSelectors()
	{
		if (_selectors != null && _selectors.Count != 0)
		{
			foreach (var selector in _selectors)
			{
				selector.QueueFree();
			}
		}
		_selectors = new List<CellSelector>();
		
		foreach (var y in Enumerable.Range(0, (int)Config.Size.Y))
		{
			foreach (var x in Enumerable.Range(0, (int)Config.Size.X))
			{
				if (x == 0 && y == 0)
				{
					CellSelector cellSelector = _cellSelectorScene.Instantiate<CellSelector>();
						
					cellSelector.Config = Config;
					
					_selectorsWrapper.AddChild(cellSelector);
					_selectors.Add(cellSelector);
				}
				else
				{
					CellSelector cellSelector = _cellSelectorScene.Instantiate<CellSelector>();
					
					cellSelector.Config = Config;
				
					cellSelector.Position = new Vector2(32 * x - 32 * y, 16 * x + 16 * y);
					
					_selectorsWrapper.AddChild(cellSelector);
					_selectors.Add(cellSelector);
				}
				
			}
		}
	}

	public override void _Process(double delta)
	{
		if (GameManager.Instance.Map == null) return;
		
		TileMapLayer map = GameManager.Instance.Map;
		
		Vector2 mousePosition = map.ToLocal(GetGlobalMousePosition());
		
		Vector2I mapPosition = map.LocalToMap(mousePosition);
		
		GlobalPosition = map.MapToLocal(mapPosition);
	}
}
