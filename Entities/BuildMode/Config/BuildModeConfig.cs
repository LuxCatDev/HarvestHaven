using Godot;
using System;
using Game.Entities.CellSelection.Config;
using Game.Entities.Objects.Placeable;

namespace Game.Entities.BuildMode.Config;

[GlobalClass]
public partial class BuildModeConfig : Resource
{
	[Export]
	public Godot.Collections.Array<CellSelectorRule> Rules { get; set; }

	[Export] public Vector2 Size { get; set; } = new(1, 1);
}
