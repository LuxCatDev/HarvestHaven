using Godot;
using System;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class CellSelectorTrait : Resource
{
    public virtual bool Validate()
    {
        return true;
    }

    public virtual void Exec()
    {
        
    }
}
