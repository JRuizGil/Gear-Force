using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : Level
{
    public GameObject starsGrid;
    private int _starsQuantity = 3;
    public GameObject emptyStar;
    public GameObject FilledStar;
    public Button button;
    public TMP_Text buttontxt;
    public GameUI gameUI;
    public List<GameObject> stars;
    public int levelIndex;
    public GearCounter gearCounter;
    private void Start()
    {
        
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        button = gameObject.GetComponentInChildren<Button>();
        button.onClick.AddListener(onclick);
        for (int i = 0; i < _starsQuantity; i++)
        {
            int obtainedStars = PlayerPrefs.GetInt("LevelStars_" + levelIndex, 0);
            UpdateStars(obtainedStars);
        }
    }

    private void onclick()
    {
        gameUI.currentlevelScriptable = levelScriptable;
        gameUI.GenerateGearboxes();
        gameUI.currentlevelText.text = "Level " + (gameUI.currentlevelScriptable.currentLevel + 1).ToString();
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

    public void UpdateStars(int obtainedStars)
    {
        // Elimina estrellas viejas
        foreach (Transform child in starsGrid.transform)
        {
            Destroy(child.gameObject);
        }

        stars.Clear();

        int totalStars = 3;

        // Instanciar estrellas llenas
        for (int i = 0; i < obtainedStars; i++)
        {
            GameObject filledStar = Instantiate(FilledStar, starsGrid.transform);
            stars.Add(filledStar);
        }
        // Instanciar estrellas vacías
        for (int i = obtainedStars; i < totalStars; i++)
        {
            GameObject emptyStarinstance = Instantiate(emptyStar, starsGrid.transform);
            stars.Add(emptyStarinstance);
        }
    }
    public int LoadLevelStars(int levelIndex)
    {
        string key = "LevelStars_" + levelIndex;
        return PlayerPrefs.GetInt(key, 0); // 0 si nunca se jugó
    }
}
