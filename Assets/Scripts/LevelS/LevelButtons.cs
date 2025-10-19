using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtons : Level
{
    public GameObject starsGrid;
    private int _starsQuantity = 3;
    public GameObject emptyStar;

    private void Start()
    {
        for (int i = 0; i < _starsQuantity; i++)
        {
            GameObject starInstance = Instantiate(emptyStar, starsGrid.transform);
        }
    }
}
