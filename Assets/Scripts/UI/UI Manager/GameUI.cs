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
    public GameObject Dropcell;
    public GridLayoutGroup gridLayoutGroup;
    public GameObject DropcellGrid;
    private DropCell[,] _cells;
    public int width = 5;
    public int height = 5;
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
                GameObject dropcellInstance = Instantiate(Dropcell, DropcellGrid.transform);
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
    
    #endregion
    
}
