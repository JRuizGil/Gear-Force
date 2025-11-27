using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearCounter : MonoBehaviour
{
    private int Gearcounter = 0;
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<TMP_Text>();
    }

    public void UpdateGearCounter()
    {
        Gearcounter = 0;

        foreach (Transform child in transform)
        {
            Gearcounter++;
        }
        _text.text = "x " + Gearcounter;
    }

}
