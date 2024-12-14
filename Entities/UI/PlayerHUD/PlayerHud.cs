using Godot;
using System;
using Game.Utilities.GameMode;

public partial class PlayerHud : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.Instance.OnModeChanged += () =>
		{
			if (GameManager.Instance.Mode != GameMode.Normal)
			{
				Visible = false;
				return;
			}

			Visible = true;
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
