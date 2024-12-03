using Game.Common.StateMachine;
using Godot;

namespace Game.Entities.Player.States;

public partial class PlayerUsingToolState: State
{
    [Export] private string _useAnimationPlaceholder = "Use_";
    
    [Export]
    private ToolManager.ToolManager _toolManager;
    
    [Export]
    private State _nextState;

    public override void Enter() {
        Player.Velocity = Vector2.Zero;
        Player.CardinalDirection = _toolManager.EquipedTool.CardinalDirection;
        Player.Textures.Scale = new Vector2(Player.CardinalDirection.X < 0 ? -1 : 1, 1);
        Player.AnimationPlayer.Play(_useAnimationPlaceholder + _toolManager.EquipedTool.AnimationDirection);
        GD.Print(_toolManager.EquipedTool.CardinalDirection, _toolManager.EquipedTool.AnimationDirection, Player.CardinalDirection);
    }
    public override void Exit() {
    }
    
    public override void HandleProcess(double delta) {

    }

    private void _onStopUsing()
    {
        StateMachine.ChangeState(_nextState);
        EmitEvent("OnStopUsing");
    }

    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}