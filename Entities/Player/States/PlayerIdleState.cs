using Game.Common.StateMachine;
using Godot;

namespace Game.Entities.Player.States;

public partial class PlayerIdleState: State
{

    public override void Enter() {
        Player.UpdateAnimation("Idle");
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
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}