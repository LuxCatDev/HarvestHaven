using Game.Common.Inventory;
using Godot;
using GodotUtilities;

namespace Game.Entities.PickableItem;

[Scene]
public partial class PickableItem : StaticBody2D
{
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }

    [Node]
    private Sprite2D _texture;

    [Node]
    private Area2D _pickingArea;

    [Node]
    public AnimationPlayer AnimationPlayer;

    [Export]
    public ItemStack Stack;

    public Vector2 Direction = Vector2.Zero;

    public Vector2 Velocity = Vector2.Zero;

    private bool _pickable;

    private bool _toPlayer;

    public override void _Ready()
    {
        _texture.Texture = Stack.ItemType.Logo;
        GetTree().CreateTimer(0.5).Timeout += () =>
        {
            _pickable = true;
            _pickingArea.Monitoring = true;
        };
    }

    public override void _PhysicsProcess(double delta)
    {
        KinematicCollision2D collision2D = MoveAndCollide(Velocity * (float)delta);

        if (collision2D != null)
        {
            Velocity = Velocity.Bounce(collision2D.GetNormal());
        }

        Velocity -= Velocity * (float)delta * 4;
    }

    public void OnPickingAreaBodyEntered(Node2D body)
    {
        if (body is Player.Player player)
        {
            ItemStack res = player.Inventory.AddItem(Stack);

            if (res != null)
            {
                Stack.Amount = res.Amount;
                _pickable = false;
                _pickingArea.Monitoring = false;
                GetTree().CreateTimer(0.5).Timeout += () =>
                {
                    _pickable = true;
                    _pickingArea.Monitoring = true;
                };
            }

            QueueFree();
        }
    }
}