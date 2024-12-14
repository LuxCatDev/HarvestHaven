using System.Collections.Generic;
using Game.Entities.ObjectInventory;
using Godot;
using GodotUtilities;

namespace Game.UI.BuildMode;

[Scene]
public partial class BuildModeUi : CanvasLayer
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}
	
	[Signal]
	public delegate void OnObjectItemSelectedEventHandler(ObjectItem item);

	[Export] public PackedScene ObjectListItemScene;
	
	[Node]
	private AnimationPlayer _animationPlayer;

	[Node("ObjectInventoryDisplay/Scroll/ObjectList")]
	private VBoxContainer _objectList;

	private bool _status = true;

	public void SetItems(List<ObjectItem> items)
	{
		if (_objectList.GetChildCount() > 0)
		{
			foreach (var child in _objectList.GetChildren())
			{
				child.QueueFree();
			}
		}

		foreach (var item in items)
		{
			ObjectListItem listItem = ObjectListItemScene.Instantiate<ObjectListItem>();

			listItem.ObjectItem = item;
			
			listItem.OnSelected += _OnSelected;
			
			_objectList.AddChild(listItem);
		}
	}

	public void Open()
	{
		_animationPlayer.Play("Open");
	}
	
	public void Close()
	{
		_animationPlayer.Play("Close");
	}

	private void _OnButtonPressed()
	{
		if (_status)
		{
			_status = false;
			_animationPlayer.Play("Close");
		}
		else
		{
			_status = true;
			_animationPlayer.Play("Open");
		}
	}

	private void _OnSelected(ObjectItem item)
	{
		EmitSignal(SignalName.OnObjectItemSelected, item);
	}
}
