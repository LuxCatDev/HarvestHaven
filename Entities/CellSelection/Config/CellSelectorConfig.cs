using Godot;
using Godot.Collections;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class CellSelectorConfig : Resource
{
	[Export]
	public Godot.Collections.Array<CellSelectorRule> Rules { get; set; }
	
	[Export]
	public Godot.Collections.Array<CellSelectorTrait> Traits { get; set; }
	
	[Export]
	public Vector2 Size { get; set; }

	public CellSelectorConfig() : this(null,null, new (1,1)) {}
	
	public CellSelectorConfig(Array<CellSelectorRule> rules, Array<CellSelectorTrait> traits, Vector2 size)
	{
		Rules = rules;
		Traits = traits;
		Size = size;
	}
}
