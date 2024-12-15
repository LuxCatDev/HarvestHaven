using Game.Entities.Objects.Placeable;
using Godot;
using Game.World.TileSets;
using Godot.Collections;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class PlaceTrait : CellSelectorTrait
{
    [Export]
    public Placeable Placeable { get; set; }
    
    public override bool Validate(CellSelectorController controller)
    {
        return controller.Valid;
    }

    public override void Exec(CellSelectorController controller)
    {
        PlaceableObject placeableObject = Placeable.Scene.Instantiate<PlaceableObject>();
        
        placeableObject.Orientation = controller.Orientation;
        placeableObject.GlobalPosition = controller.Preview.GlobalPosition;
        placeableObject.Preview = false;
        
        GameManager.Instance.Level.AddChild(placeableObject);
    }
}