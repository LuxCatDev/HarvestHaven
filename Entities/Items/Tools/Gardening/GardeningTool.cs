using Godot;
using System;
using Game.Entities.CellSelection.Config;
using GodotUtilities;

namespace Game.Entities.Items.Tools.Gardening;

[Scene]
public partial class GardeningTool : Tool
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}
	
	[Export] private CellSelectorConfig _selectorConfig;
	
	[Node]
	public AnimationPlayer AnimationPlayer;

	[Node("Texture")] private Sprite2D _texture;
	
	public override string AnimationDirection
	{
		get
		{
			if (CardinalDirection == Vector2.Down)
			{
				return "S";
			} else if (CardinalDirection == Vector2.Up)
			{
				return "N";
			} else if (CardinalDirection.Y == 0 && CardinalDirection.X != 0)
			{
				return "W";
			} else if (CardinalDirection.X != 0 && CardinalDirection.Y > 0)
			{
				return "SW";
			}
			else
			{
				return "NW";
			}
		}
	}

	public override void UpdateAnimation(string anim)
	{
		AnimationPlayer.Play(anim);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimationPlayer.AnimationFinished += _onAnimationFinished;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnContact()
	{
		GameManager.Instance.Player.CellSelectorController.Config = _selectorConfig;
		GameManager.Instance.Player.CellSelectorController.ExecTraits();
	}

	public override void Use()
	{
		if (!GameManager.Instance.Player.CellSelectorController.Valid) return;
		
		Vector2 groupPosition = GameManager.Instance.Player.CellSelectorController.GroupPosition;
		Vector2 direction = GameManager.Instance.Player.GlobalPosition.DirectionTo(groupPosition);

		CardinalDirection = direction.Snapped(Vector2.One);
		EmitSignal(Tool.SignalName.OnUsed);
		AnimationPlayer.Play("Use_" + AnimationDirection);
	}

	public override void Equip(){
		GameManager.Instance.Player.CellSelectorController.Config = _selectorConfig;
		GameManager.Instance.Player.CellSelectorController.Enable();
	}

	public override void UnEquip()
	{
		GameManager.Instance.Player.CellSelectorController.Config = null;
		GameManager.Instance.Player.CellSelectorController.Disable();
	}
	
	private void _onAnimationFinished(Godot.StringName anim)
	{
		if (anim.ToString().StartsWith("Use"))
		{
			EmitSignal(Tool.SignalName.OnStopUsing);
		}
	}
}
