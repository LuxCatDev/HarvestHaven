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

	public override void UpdateAnimation(string anim)
	{
		AnimationPlayer.Play(anim);
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Use()
	{
		
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
}
