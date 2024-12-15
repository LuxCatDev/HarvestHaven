using Godot;

namespace Game.Entities.CellSelection.Config.Rules;

[GlobalClass]
public partial class IsNotOverPlayerRule: CellSelectorRule
{
    public override bool Validate(CellSelector.CellSelector selector)
    {
        return !selector.IsOverPlayer;
    }
}