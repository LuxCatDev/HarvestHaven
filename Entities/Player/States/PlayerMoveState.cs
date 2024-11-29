using Game.Common.StateMachine;
using Godot;

namespace Game.Entities.Player.States;

public partial class PlayerMoveState: State
{

    [Export]
    private int _moveSpeed = 100;


    public override void Enter() {
        Player.UpdateAnimation("Walk");
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
            Player.UpdateAnimation("Walk");
        }
    }
    public override void HandlePhysicsProcess(double delta) {

    }
    public override void HandleInput(InputEvent @event) {

    }
}