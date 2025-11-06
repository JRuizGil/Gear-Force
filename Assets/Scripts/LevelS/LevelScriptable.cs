using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/SpawnLevelScriptable", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public int currentLevel = 0;
    public float levelTime = 60f;
    public int gears = 10;
    public bool isLevelCompleted = false;
    public int _gearSlotsX = 10;
    public int _gearSlotsY = 10;
    public int gearstopower = 1;
    public int PoweredPosition = 1;
    public int[] UnPoweredPositions = new int[1];
}
