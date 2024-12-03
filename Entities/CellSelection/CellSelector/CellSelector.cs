using System.Linq;
using Godot;
using Game.Entities.CellSelection.Config;
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

	[Export] public CellSelectorConfig Config;

	public Vector2I TilePosition;

	private bool _state;

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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
}
