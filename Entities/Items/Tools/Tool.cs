using Godot;

namespace Game.Entities.Items.Tools;

public partial class Tool : Node2D
{
    [Signal]
    public delegate void OnUsedEventHandler();
    
    [Signal]
    public delegate void OnStopUsingEventHandler();
    
    public Vector2 CardinalDirection = Vector2.Zero;

    public virtual string AnimationDirection
    {
        get => "";
    }
    
    public virtual void UpdateAnimation(string animation)
    {
        
    }
    
    public virtual void Equip(){

    }

    public virtual void UnEquip()
    {

    }

    public virtual void Use()
    {
        
    }
}
