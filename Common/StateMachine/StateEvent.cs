using Godot;

namespace Game.Common.StateMachine;

public partial class StateEvent: Node
{
    [Signal]
    public delegate void EmitedEventHandler();

    public void Emit()
    {
        EmitSignal(SignalName.Emited);
    }
}