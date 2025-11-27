using System;
using UnityEngine;

public class Divider : Gear
{
    private Gear gear;
    private void OnEnable()
    {
        gear = gameObject.GetComponent<Gear>();
    }

    
}
