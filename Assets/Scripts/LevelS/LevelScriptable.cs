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
    public int PoweredPosition;
    public int[] UnPoweredPositions = new int[1];
    
    
    public bool bDivider;
    public bool bTrivider;
    public bool bDiagonalGear;
    public bool bBlocker;
    public bool bTeleporter;
    
    public Vector2 DividerPosition;
    public Vector2 TrividerPosition;
    public Vector2 DiagonalGearPosition;
    public Vector2 BlockerPosition;
    public Vector2 TeleporterPosition;
    
    
    public GameObject Divider;
    public GameObject Trivider;
    public GameObject DiagonalGear;
    public GameObject Blocker;
    public GameObject Teleporter;
}
