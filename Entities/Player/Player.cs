using Game.Common.Inventory;
using Game.Common.StateMachine;
using Game.Entities.BuildMode;
using Game.Entities.CellSelection;
using Godot;
using GodotUtilities;

namespace Game.Entities.Player;

[Scene]
public partial class Player : CharacterBody2D
{
    public override void _Notification(int what)
    {
        if (what == NotificationSceneInstantiated)
        {
            WireNodes(); // this is a generated method
        }
    }
    
    // Nodes
    
    [Node]
    public AnimationPlayer AnimationPlayer;
    
    [Node]
    public StateMachine StateMachine;
    
    [Node("Components/Inventory")]
    public Inventory Inventory;
    
    [Node("Components/ToolInventory")]
    public Inventory ToolInventory;
    
    [Node("Components/ToolManager")]
    public ToolManager.ToolManager ToolManager;
    
    [Node("Components/CellSelectorController")]
    public CellSelectorController CellSelectorController;
    
    [Node("Components/BuildModeController")]
    public BuildModeController BuildModeController;

    [Node] public Node2D Textures;
    
    // Movement

    private Vector2 _direction;

    public Vector2 Direction => _direction;

    public Vector2 CardinalDirection
    {
        get;
        set;
    }

    public string AnimationDirection
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

    public override void _Ready()
    {
        CardinalDirection = Vector2.Down;
        GameManager.Instance.Player = this;
        StateMachine.Init();
    }

    public override void _Process(double delta)
    {
        _direction = new Vector2(Input.GetAxis("left", "right"), Input.GetAxis("up", "down")).Normalized();
    }
    
    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

    public bool SetDirection()
    {
        if (_direction == Vector2.Zero) return false;

        Vector2 newCardinalDirection = _direction.Snapped(Vector2.One);

        if (newCardinalDirection == CardinalDirection) return false;

        CardinalDirection = newCardinalDirection;

        Textures.Scale = new Vector2(CardinalDirection.X < 0 ? -1 : 1, 1);
        
        return true;
    }
    
    public void UpdateAnimation(string anim)
    {
        AnimationPlayer.Play(anim + "_" + AnimationDirection);
    }
}
