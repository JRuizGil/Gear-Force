using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/SpawnLevelScriptable", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public int currentLevel = 0;
    public int starsObtained = 0;
    public float levelTime = 60f;
    public float timeElapsed;
    public int gears = 10;
    public int gearsUsed = 0;
    public bool isLevelCompleted = false;
}
