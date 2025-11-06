using System;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : Level
{
    public GameObject starsGrid;
    private int _starsQuantity = 3;
    public GameObject emptyStar;
    public Button button;
    public TMP_Text buttontxt;
    public GameUI gameUI;
    
    private void Start()
    {
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(onclick);
        for (int i = 0; i < _starsQuantity; i++)
        {
            GameObject starInstance = Instantiate(emptyStar, starsGrid.transform);
        }
    }

    private void onclick()
    {
        gameUI.currentlevelScriptable = levelScriptable;
        gameUI.currentlevelText.text = "Level " + ((gameUI.currentlevelScriptable.currentLevel).ToString());
        gameUI.gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gameUI.gridLayoutGroup.constraintCount = levelScriptable._gearSlotsX;
        gameUI.width = levelScriptable._gearSlotsX;
        gameUI. height = levelScriptable._gearSlotsY;
        gameUI.GenerateDropcellsGrid();
        gameUI.GenerateUnPoweredGrid();
        gameUI.GeneratePoweredGrid();
        gameUI.GenerateLevelGears();
        gameUI.Show();
    }
}
