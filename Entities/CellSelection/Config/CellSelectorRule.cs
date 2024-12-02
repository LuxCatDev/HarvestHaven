using Godot;
using System;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class CellSelectorRule : Resource
{
	public virtual bool Validate(CellSelector.CellSelector selector)
	{
		return true;
	}
}
