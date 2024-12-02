using System.Collections.Generic;
using Game.Entities.Player;
using Godot;

namespace Game.Common.StateMachine;

public partial class State: Node
{
    public bool Active;
    
    public Player Player;

    public StateMachine StateMachine;

    private List<StateEvent> _events;

    [Export]
    private Godot.Collections.Array<StateEvent> _launchEvents;

    public void Init()
    {
        Active = false;
        _events = new();
        
        foreach(StateEvent stateEvent in _launchEvents)
        {
            stateEvent.Emited += () => StateMachine.ChangeState(this);
        }

        foreach(Node child in GetChildren())
        {
            if (child is StateEvent stateEvent)
            {
                _events.Add(stateEvent);
            }
        }
    }

    public virtual void Enter() {
    }
    public virtual void Exit() {

    }
    public virtual void HandleProcess(double delta) {

    }
    public virtual void HandlePhysicsProcess(double delta) {

    }
    public virtual void HandleInput(InputEvent @event) {

    }

    public void EmitEvent(string eventName)
    {
        foreach(StateEvent stateEvent in _events)
        {
            if (stateEvent.Name == eventName)
            {
                stateEvent.Emit();
            }
        }
    }

}