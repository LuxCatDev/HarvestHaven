using Game.Entities.Player;
using Game.Utilities.GameMode;
using Godot;

public partial class GameManager : Node
{
    [Signal]
    public delegate void OnModeChangedEventHandler();
    
    public static GameManager Instance { get; private set; }

    public Player Player { get; set; }
    public TileMapLayer Map { get; set; }
    
    public GameMode Mode { get; private set; }
    
    public Node2D Level { get; set; }

    public void ChangeMode(GameMode mode)
    {
        Mode = mode;
        EmitSignal(SignalName.OnModeChanged);
    }

    public override void _Ready()
    {
        Instance = this;
    }
}