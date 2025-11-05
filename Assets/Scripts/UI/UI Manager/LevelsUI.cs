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
    
    #region levels Implementation
    public override void Initialize()
    {
        base.Initialize();
        for (int i = 0; i < levelScriptableList.Count; i++)
        {
            GameObject buttonInstance = Instantiate(LevelButton, LevelsGrid.transform);
            buttonInstance.GetComponent<LevelButtons>().levelScriptable = levelScriptableList[i];
            var levelButton = buttonInstance.GetComponent<LevelButtons>();
            levelButton.buttontxt.text = $"Level {levelButton.levelScriptable.currentLevel}";
        }
    }
    
    #endregion
}
