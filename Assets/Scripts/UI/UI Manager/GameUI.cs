using Unity.VisualScripting;
using UnityEngine;

public class GameUI : UIWindow
{
    #region Game Implementation

    private GameObject GearsGrid;
    public GameObject DropCellPrefab;
    private int _gearSlotsX = 5;
    private int _gearSlotsY = 5;

    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < _gearSlotsX; i++)
        {
            for (int j = 0; j < _gearSlotsY; j++)
            {
                var dropcell = Instantiate(DropCellPrefab, GearsGrid.transform);
                int slotX = i;
            }
            
        }

    }

    #endregion
}
