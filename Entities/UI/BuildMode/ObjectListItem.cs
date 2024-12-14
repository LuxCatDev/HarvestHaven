using Game.Entities.BuildMode;
using Godot;
using Game.Entities.ObjectInventory;
using Game.Entities.Objects.Placeable;
using GodotUtilities;

[Scene]
public partial class ObjectListItem : Control
{

	[Signal]
	public delegate void OnSelectedEventHandler(ObjectItem item);
	
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated)
		{
			WireNodes(); // this is a generated method
		}
	}

	[Export] public ObjectItem ObjectItem;

	[Node("Icon/Texture")] public TextureRect Icon;

	[Node("Value/Label")] public Label ValueLabel;

	[Node("Name")] public Label NameLabel;
	[Node("Description")] public Label DescriptionLabel;
	
	[Node] public TextureButton BuildButton;
	[Node] public TextureButton SelectedButton;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Icon.Texture = ObjectItem.Object.Icon;
		ValueLabel.Text = ObjectItem.Object.Value.ToString();
		NameLabel.Text = ObjectItem.Object.Name;
		DescriptionLabel.Text = ObjectItem.Object.Description;
		GameManager.Instance.Player.BuildModeController.Connect(BuildModeController.SignalName.OnSelectedObjectChanged, Callable.From(_OnSelectedObjectChanged));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _OnSelectedObjectChanged()
	{
		if (GameManager.Instance.Player.BuildModeController.ItemSelected == ObjectItem)
		{
			BuildButton?.Hide();
			SelectedButton?.Show();
		}
		else
		{
			BuildButton?.Hide();
			SelectedButton?.Show();
		}
	}

	private void _OnButtonPressed()
	{
		EmitSignal(SignalName.OnSelected, ObjectItem);
	}
}
