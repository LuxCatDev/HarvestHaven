using Game.Common.StateMachine;
using Godot;

public partial class OnToolUsed : StateEvent
{
	private void _onToolUsed()
	{
		Emit();
	}
}
