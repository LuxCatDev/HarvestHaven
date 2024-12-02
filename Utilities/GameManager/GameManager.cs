using Game.Entities.Player;
using Godot;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; }

    public Player Player { get; set; }
    public TileMapLayer Map { get; set; }

    public override void _Ready()
    {
        Instance = this;
    }
}