using Godot;
using System;
using System.Collections.Generic;
using GodotUtilities;

[Scene]
public partial class PlaceableObject : Node2D
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Node] private Sprite2D _texture;
	[Node] private AnimationPlayer _animationPlayer;
	[Node("CollisionShape")] private CollisionPolygon2D _collision;

	[Export] private Color _previewColor;

	private bool _preview;

	public bool Preview
	{
		set
		{
			if (value)
			{
				_collision.Disabled = true;
				_texture.Modulate = _previewColor;
			}
			else
			{
				_collision.Disabled = false;
				_texture.Modulate = Colors.White;
			}
			
			_preview = value;
		}
		get => _preview;
	}


	public List<Vector2> Orientations = new() {Vector2.Down, Vector2.Left, Vector2.Up, Vector2.Right};
	public List<string> OrientationsName = new() {"S", "E", "N", "W"};

	private Vector2 _orientation = Vector2.Down;

	public Vector2 Orientation
	{
		get => _orientation;
		set
		{
			_orientation = value;
			_animationPlayer.Play(OrientationName);
		}
	}

	public string OrientationName
	{
		get
		{
			var index = Orientations.FindIndex(i => i == _orientation);
			
			return index == -1 ? string.Empty : OrientationsName[index];
		}
	}

	public override void _Ready()
	{
		_animationPlayer.Play(OrientationName);
	}
}
