using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class LevelsUI : UIWindow
{
    public List<LevelScriptable> levelScriptableList;
    public GameObject LevelsGrid;
    public GameObject LevelButton;
    public int currentLevel = 0;
    public GameUI gameUI;
    public List<GameObject> levelButtons;
    
    #region levels Implementation
    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < levelScriptableList.Count; i++)
        {
            GameObject buttonInstance = Instantiate(LevelButton, LevelsGrid.transform);
            levelButtons.Add(buttonInstance);
            buttonInstance.GetComponent<LevelButtons>().levelScriptable = levelScriptableList[i];
            var levelButton = buttonInstance.GetComponent<LevelButtons>();
            levelButton.levelIndex = i;
            levelButton.buttontxt.text = $"Level {levelButton.levelScriptable.currentLevel}";
        }
    }
    public void SaveLevelStars(int levelIndex, int obtainedStars)
    {
        string key = "LevelStars_" + levelIndex;

        // Obtiene la mejor puntuación previa
        int previousStars = PlayerPrefs.GetInt(key, 0);

        // Guarda solo si es mejor
        if (obtainedStars > previousStars)
        {
            PlayerPrefs.SetInt(key, obtainedStars);
            PlayerPrefs.Save(); // opcional pero recomendable
        }
    }
    

    #endregion
}
