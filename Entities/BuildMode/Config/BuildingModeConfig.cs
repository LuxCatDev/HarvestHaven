using Godot;
using System;
using Game.Entities.CellSelection.Config;
using Game.Entities.Objects.Placeable;

namespace Game.Entities.BuildingMode.Config;

[GlobalClass]
public partial class BuildingModeConfig : Resource
{
	[Export]
	public CellSelectorConfig CellSelectorConfig { get; set; }
}
