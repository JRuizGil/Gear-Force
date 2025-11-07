using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DropCell : MonoBehaviour
{
    public int slotX;
    public int slotY;
    public Button button;
    public bool btnenabled = false;
    private Image _image;
    private GameUI _gameUI;
    
    void Start()
    {
        
        _gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        _image = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnDropcellclick);
        btnenabled = false;
        SetColocationEnabled(false);
    }
    private void OnDropcellclick()
    {
        if (!btnenabled)
        {
            RectTransform rt = _image.GetComponent<RectTransform>();
            rt.DOKill(true);
            rt.DOShakeAnchorPos(1f, new Vector2(10f, 0), 10, 0, false, true);
            return;
        }
    
        Gear gear = _gameUI.SelectedGear;
        if (gear == null) return;
    
        gear.transform.SetParent(transform, false);
        gear.transform.localPosition = Vector3.zero;

        gear.CheckPosition(); // ✅ Actualiza slotX / slotY / isOnDropcell

        _gameUI.ClearSelection();

        // ✅ AHORA recalculamos TODO el mapa de energía
        _gameUI.RecalculatePower();
    }
    public void SetColocationEnabled(bool enabledbutton)
    {
        btnenabled = enabledbutton;
        _image.color = enabledbutton ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.6f);
    }
    

    
    
}
