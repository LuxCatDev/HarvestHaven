using Godot;
using System;
using GodotUtilities;

[Scene]
public partial class Playground : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}
	
	[Node]
	public TileMapLayer TileMapLayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameManager.Instance.Map = TileMapLayer;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
