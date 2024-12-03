using Game.World.TileSets;
using Godot;
using Godot.Collections;

namespace Game.Entities.CellSelection.Config;

[GlobalClass]
public partial class TerrainRule: CellSelectorRule
{
    
    TerrainRule() : this(null) {}
    
    public TerrainRule(Array<TerrainType> allowedTerrains)
    {
        AllowedTerrains = allowedTerrains;
    }

    [Export]
    public Godot.Collections.Array<TerrainType> AllowedTerrains { get; set; }
    
    public override bool Validate(CellSelector.CellSelector selector)
    {
        TileData data = GameManager.Instance.Map.GetCellTileData(selector.TilePosition);

        if (data == null) return false;

        int terrainId = data.GetCustomData("TerrainType").As<int>();

        bool res = false;

        foreach (TerrainType terrainType in AllowedTerrains)
        {
            if ((int)terrainType == terrainId)
            {
                res = true;
            }
        }

        return res;
    }
}