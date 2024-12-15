using System.Collections.Generic;
using System.Linq;
using Godot;
using Game.Entities.CellSelection.Config;
using Game.World.TileSets;
using GodotUtilities;

namespace Game.Entities.CellSelection.CellSelector;

[Scene]
public partial class CellSelector : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node("Texture")] private Sprite2D _texture;
	
	[Node("AnimationPlayer")] private AnimationPlayer _animationPlayer;

	[Node] private Area2D _detectionArea;

	[Export] public CellSelectorConfig Config;

	public Vector2I TilePosition;

	private bool _state;
	
	private List<Node2D> _collidingWith;

	public bool State
	{
		get
		{
			return _state;
		}
		private set
		{
			if (value)
			{
				_animationPlayer.Play("Normal");
			}
			else
			{
				_animationPlayer.Play("Wrong");
			}
			
			_state = value;
		}
	}

	public bool IsOverPlayer
	{
		get
		{
			foreach (var collider in _collidingWith)
			{
				if (collider is Player.Player player)
				{
					return true;
				}
			}

			return false;
		}
	}

	public bool IsColliding => _collidingWith.Count > 0;

	public TerrainType TerrainType
	{
		get
		{
			TileData data = GameManager.Instance.Map.GetCellTileData(TilePosition);

			if (data == null) return TerrainType.Dirt;
			
			return data.GetCustomData("TerrainType").As<TerrainType>();
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_collidingWith = new List<Node2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (GameManager.Instance.Map == null) return;
		
		TileMapLayer map = GameManager.Instance.Map;
		
		Vector2 localPosition = map.ToLocal(GlobalPosition);
		
		Vector2I mapPosition = map.LocalToMap(localPosition);

		if (TilePosition != mapPosition)
		{
			TilePosition = mapPosition;
			Update();
		}
		
		TilePosition = mapPosition;
	}

	private void _validate()
	{
		foreach (var rule in Config.Rules)
		{
			if (!rule.Validate(this))
			{
				State = false;
				return;
			}
		}

		State = true;
	}

	public void Update()
	{
		_validate();
	}

	private void _on_Area2D_body_entered(Node2D body)
	{
		_collidingWith.Add(body);
		Update();
	}
	
	private void _on_Area2D_area_entered(Area2D area)
	{
		_collidingWith.Add(area);
		Update();
	}
	
	private void _on_Area2D_body_exited(Node2D body)
	{
		_collidingWith.Remove(body);
		Update();
	}
	
	private void _on_Area2D_area_exited(Area2D area)
	{
		_collidingWith.Remove(area);
		Update();
	}
}
