using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameUI : UIWindow
{
    #region Game Implementation
    
    public LevelScriptable currentlevelScriptable;
    public TMP_Text currentlevelText;
    public GameObject DropcellPrefab;
    public GameObject GearPrefab;
    public GameObject GearToPower;
    public GameObject DropcellGrid;
    public GameObject PoweredGrid;
    public GameObject PoweredCellPrefab;
    public GameObject UnPoweredGrid;
    public GameObject UnPoweredCellPrefab;
    public Transform GearBox;
    public GridLayoutGroup gridLayoutGroup;
    public DropCell[,] _cells;
    public int width = 5;
    public int height = 5;
    public Gear SelectedGear { get; private set; }
    public override void Initialize()
    {
        base.Initialize();
    }
    public void GenerateDropcellsGrid()
    {
        if (gridLayoutGroup == null)
            gridLayoutGroup = DropcellGrid.GetComponent<GridLayoutGroup>();

        foreach (Transform child in DropcellGrid.transform)
        {
            Destroy(child.gameObject);
        }

        _cells = new DropCell[width, height];
        
        AdjustCellSize(width, height);

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = width;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject dropcellInstance = Instantiate(DropcellPrefab, DropcellGrid.transform);
                DropCell dropCell = dropcellInstance.GetComponent<DropCell>();
                dropCell.slotX = x;
                dropCell.slotY = y;
                _cells[x, y] = dropCell;
            }
        }
    }
    private void AdjustCellSize(int width, int height)
    {
        RectTransform rt = gridLayoutGroup.GetComponent<RectTransform>();
        float cellWidth = rt.rect.width / width;
        float cellHeight = rt.rect.height / height;
        gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
    }
    
    public DropCell GetCell(int x, int y)
    {
        if (x < 0 || y < 0 || x >= _cells.GetLength(0) || y >= _cells.GetLength(1))
            return null;

        return _cells[x, y];
    }

    public void GenerateLevelGears()
    {
        foreach (Transform gear in GearBox.transform)
        {
            Destroy(gear.gameObject);
        }
        for (int i = 0; i < currentlevelScriptable.gears; i++)
        {
            GameObject GearInstance = Instantiate(GearPrefab, GearBox);
        }
    }
    public void SelectGear(Gear gear)
    {
        SelectedGear = gear;
        foreach (DropCell cell in _cells)
        {
            cell.SetColocationEnabled(true);
        }
    }
    public void ClearSelection()
    {
        if (SelectedGear != null)
        {
            SelectedGear.isOnDropcell = true;
        }

        SelectedGear = null;

        foreach (DropCell cell in _cells)
        {
            cell.SetColocationEnabled(false);
        }
    }
    #endregion
}
