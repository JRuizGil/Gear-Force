using System;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameUI : UIWindow
{
    #region Game Implementation
    
    [Header("Game UI")]
    public LevelScriptable currentlevelScriptable;
    public TMP_Text currentlevelText;
    
    public GameObject DropcellPrefab;
    public GameObject DropcellGrid;
    
    public GameObject GearPrefab;
    
    public GameObject PoweredGrid;
    public HorizontalLayoutGroup PoweredHorizontalLayoutGroup;
    public GameObject PoweredCellPrefab;
    public GameObject GearPoweredPrefab;
    
    public GameObject UnPoweredGrid;
    public HorizontalLayoutGroup UnPoweredHorizontalLayoutGroup;
    public GameObject UnPoweredCellPrefab;
    public GameObject GearToPowerPrefab;
    
    public List<GearToPower> _UnpoweredGears = new List<GearToPower>();
    public bool currentleveliscompleted = false;
    
    public Transform GearBox;
    
    public GridLayoutGroup gridLayoutGroup;
    
    public DropCell[,] _cells;
    public int width = 5;
    public int height = 5;
    public Gear SelectedGear { get; private set; }
    public PoweredGear currentPoweredGear;
    private UIManager uiManager;
    public CompletedUI completedUI;
    
    public TimerController timer;
    public LevelsUI levelsUI;

    public override void Initialize()
    {
        base.Initialize();
        uiManager = FindFirstObjectByType<UIManager>();
        levelsUI = FindFirstObjectByType<LevelsUI>();

    }
    public void GenerateDropcellsGrid()
    {
        if (gridLayoutGroup == null)
            gridLayoutGroup = DropcellGrid.GetComponent<GridLayoutGroup>();

        foreach (Transform child in DropcellGrid.transform)
        {
            Destroy(child.gameObject);
        }
        currentleveliscompleted = false;
        _cells = new DropCell[width, height];
        
        AdjustCellSize(width, height);

        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = width;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject dropcellInstance = Instantiate(DropcellPrefab, DropcellGrid.transform);
                DropCell dropCell = dropcellInstance.GetComponent<DropCell>();
                dropCell.slotX = x;
                dropCell.slotY = y;
                _cells[x, y] = dropCell;
            }
        }
        timer.ResetTimer();
        timer.StartTimer();
    }

    public void GeneratePoweredGrid()
    {
        foreach (Transform child in PoweredGrid.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < currentlevelScriptable._gearSlotsX; i++)
        {
            GameObject poweredCell = Instantiate(PoweredCellPrefab, PoweredGrid.transform);
            if (i == currentlevelScriptable.PoweredPosition)
            {
                GameObject poweredgearinstance = Instantiate(GearPoweredPrefab, poweredCell.transform);
                PoweredGear getPoweredgear = poweredgearinstance.GetComponent<PoweredGear>();
                getPoweredgear.Xposition = i;
                currentPoweredGear = getPoweredgear;
            }
        }
    }

    public void GenerateUnPoweredGrid()
    {
        foreach (Transform child in UnPoweredGrid.transform)
        {
            Destroy(child.gameObject);
        }
        _UnpoweredGears.Clear();
        
        for (int i = 0; i < currentlevelScriptable._gearSlotsX; i++)
        {
            GameObject unPoweredCell = Instantiate(UnPoweredCellPrefab, UnPoweredGrid.transform);
            if (Array.Exists(currentlevelScriptable.UnPoweredPositions, pos => pos == i))
            {
                GameObject unpoweredinstance = Instantiate(GearToPowerPrefab, unPoweredCell.transform);
                GearToPower gear = unpoweredinstance.GetComponent<GearToPower>();
                gear.Xposition = i;

                _UnpoweredGears.Add(gear); // ✅ Guardamos solo los de destino
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
    public void RecalculatePower()
    {
        // 1) Apagar todos los gears del grid
        for (int x = 0; x < _cells.GetLength(0); x++)
        {
            for (int y = 0; y < _cells.GetLength(1); y++)
            {
                DropCell cell = _cells[x, y];
                if (cell == null) continue;
                Gear g = cell.GetComponentInChildren<Gear>();
                if (g != null)
                {
                    g.ispowered = false;
                }
            }
        }

        // 2) Buscar el PoweredGear (puedes cachearlo en GeneratePoweredGrid para mejor perf.)
        PoweredGear powered = currentPoweredGear;
        if (powered == null) return;

        int px = powered.Xposition;
        int py = 0; // la fila donde comienza la alimentación en tu grid

        // 3) Obtener la celda de inicio en el grid
        DropCell startCell = GetCell(px, py);
        if (startCell == null) return;

        // 4) Obtener el gear que esté en esa celda (si lo hay)
        Gear rootGear = startCell.GetComponentInChildren<Gear>();
        if (rootGear == null) return;

        // 5) Encender el gear inicial y propagar
        rootGear.ispowered = true;

        // Opcional: sincronizar la dirección con el PoweredGear
        // Si quieres que el gear en la celda gire en sentido contrario al PoweredGear, usa:
        rootGear.direction = -powered.direction;
        // Si prefieres una dirección fija, puedes poner rootGear.direction = 1;
        
        rootGear.SpreadPower();
        CheckIfLevelCompleted();
    }
    public GearToPower GetUnPoweredGearAtColumn(int x)
    {
        if (!Array.Exists(currentlevelScriptable.UnPoweredPositions, pos => pos == x))
            return null;

        Transform cell = UnPoweredGrid.transform.GetChild(x);
        if (cell == null) return null;

        return cell.GetComponentInChildren<GearToPower>();
    }

    
    public void CheckIfLevelCompleted()
    {
        if (currentleveliscompleted) return;

        foreach (var gear in _UnpoweredGears)
        {
            if (!gear.ispowered)
                return; // si uno aún no tiene power → no completar todavía
        }
        currentleveliscompleted = true;
        uiManager.ShowUI("CompletedUI");
        int calculatedstars = CalculateObtainedStars();
        completedUI.ShowStars(calculatedstars);
        GameObject levelbuttoninstance = levelsUI.levelButtons[currentlevelScriptable.currentLevel];
        LevelButtons buttoncs = levelbuttoninstance.GetComponent<LevelButtons>();
        buttoncs.UpdateStars(calculatedstars);
        levelsUI.SaveLevelStars(currentlevelScriptable.currentLevel, calculatedstars);
        
    }

    public int CalculateObtainedStars()
    {
        timer.PauseTimer();
        float timeLeft = timer.GetCurrentTime();
        
        if (timeLeft < 0) timeLeft = 0;
        
        float percentage = timeLeft / 60f;

        if (percentage > 0.7f)
            return 3;
        else if (percentage > 0.4f)
            return 2;
        else if (percentage > 0f)
            return 1;
        else
            return 0;
    }
   
    
    #endregion
}
