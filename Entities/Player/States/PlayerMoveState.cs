using Game.Common.StateMachine;
using Godot;

namespace Game.Entities.Player.States;

public partial class PlayerMoveState: State
{
    [Export]
    private int _moveSpeed = 100;
    
    [Export]
    private ToolManager.ToolManager _toolManager;

    public override void Enter() {
        Player.UpdateAnimation(_toolManager.EquipedTool == null ? "Walk" : "Walk_Tool");
    }
    public override void Exit() {
    }
    
    public override void HandleProcess(double delta) {
        if (Player.Direction == Vector2.Zero)
        {
            EmitEvent("OnPlayerStopMoving");
        }

        Player.Velocity = Player.Direction * _moveSpeed;

        if (Player.SetDirection())
        {
            Player.UpdateAnimation(_toolManager.EquipedTool == null ? "Walk" : "Walk_Tool");

            if (_toolManager.EquipedTool != null)
            {
                _toolManager.UpdateToolAnimation("Walk_Tool");
            }
        }
    }

    private void _onToolEquiped()
    {
        if (Active)
        {
            Player.UpdateAnimation("Walk_Tool");
            _toolManager.UpdateToolAnimation("Walk_Tool");
        }
    }
    
    private void _onUnEquipTool()
    {
        if (Active)
        {
            Player.UpdateAnimation("Walk");
        }
    }
    
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}