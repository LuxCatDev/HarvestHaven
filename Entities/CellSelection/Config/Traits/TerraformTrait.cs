using Godot;
using Game.World.TileSets;
using Godot.Collections;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class TerraformTrait : CellSelectorTrait
{
    
    [Export]
    public TerrainType TerraformType = TerrainType.Grass;
    
    public override bool Validate(CellSelectorController controller)
    {
        return controller.Valid;
    }

    public override void Exec(CellSelectorController controller)
    {
        Array<Vector2I> cells = new Array<Vector2I>();

        foreach (var selector in controller.Selectors)
        {
            cells.Add(selector.TilePosition);
        }
        
        GameManager.Instance.Map.SetCellsTerrainConnect(cells, 0, (int)TerraformType);
        controller.Update();
    }
}