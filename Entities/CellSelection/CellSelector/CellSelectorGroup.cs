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

	[Export] public Vector2 Size = new(1, 1);
	[Export] private PackedScene _cellSelectorScene;

	[Node("Selectors")] private Node2D _selectors;
	
	[Export] public CellSelectorConfig Config;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var y in Enumerable.Range(0, (int)Size.Y))
		{
			foreach (var x in Enumerable.Range(0, (int)Size.X))
			{
				if (x == 0 && y == 0)
				{
					CellSelector cellSelector = _cellSelectorScene.Instantiate<CellSelector>();
						
					cellSelector.Config = Config;
					
					_selectors.AddChild(cellSelector);
				}
				else
				{
					CellSelector cellSelector = _cellSelectorScene.Instantiate<CellSelector>();
					
					cellSelector.Config = Config;
				
					cellSelector.Position = new Vector2(32 * x - 32 * y, 16 * x + 16 * y);
					
					_selectors.AddChild(cellSelector);
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
