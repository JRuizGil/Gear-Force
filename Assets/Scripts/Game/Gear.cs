using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Gear : MonoBehaviour
{
    public GameUI gameUI;
    public Button button;
    public bool isOnDropcell;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnGearclick);
        gameUI = FindFirstObjectByType<GameUI>();
    }
    private void OnGearclick()
    {
        gameUI.SelectGear(this);
    }
    
}
