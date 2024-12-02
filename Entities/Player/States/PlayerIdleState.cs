using Game.Common.StateMachine;
using Godot;

namespace Game.Entities.Player.States;

public partial class PlayerIdleState: State
{
    
    [Export]
    private ToolManager.ToolManager _toolManager;

    public override void Enter() {
        Player.UpdateAnimation(_toolManager.EquipedTool == null ? "Idle" : "Idle_Tool");

        if (_toolManager.EquipedTool != null)
        {
            _toolManager.UpdateToolAnimation("Idle_Tool");
        }
    }
    public override void Exit() {
    }
    public override void HandleProcess(double delta) {
        if (Player.Direction != Vector2.Zero)
        {
            EmitEvent("OnPlayerMove");
        }

        Player.Velocity = Vector2.Zero;
    }
    
    private void _onToolEquiped()
    {
        if (Active)
        {
            Player.UpdateAnimation("Idle_Tool");
            _toolManager.UpdateToolAnimation("Idle_Tool");
        }
    }
    
    private void _onUnEquipTool()
    {
        if (Active)
        {
            Player.UpdateAnimation("Idle");
        }
    }
    
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}