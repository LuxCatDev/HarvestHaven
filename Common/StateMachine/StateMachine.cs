using Godot;
using System.Collections.Generic;
using Game.Entities.Player;
// ReSharper disable All

namespace Game.Common.StateMachine;

public partial class StateMachine : Node
{

    [Export]
    private Player _player;

    [Export]
    private State _initialState;

    private List<State> _states;

    private State _lastState;

    private State _currentState;

    public void Init()
    {
        _states = new();

        foreach(Node child in GetChildren())
        {
            if (child is State state)
            {
                state.Init();
                state.Player = _player;
                state.StateMachine = this;
                _states.Add(state);
            }
        }

        ChangeState(_initialState);
    }

    public void ChangeState(State state)
    {
        if (state == null) return;

        if (_currentState != null)
        {
            _lastState = _currentState;

            _currentState.Exit();
        }

        _currentState = state;

        state.Enter();
    }

    public override void _Process(double delta)
    {
        if (_currentState == null) return;
        _currentState.HandleProcess(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_currentState == null) return;
        _currentState.HandlePhysicsProcess(delta);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_currentState == null) return;
        _currentState.HandleInput(@event);
    }
}