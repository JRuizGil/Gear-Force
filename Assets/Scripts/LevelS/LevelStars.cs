using System;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelStars : MonoBehaviour
{
    public GameObject filledStar;

    private void Start()
    {
        filledStar.SetActive(false);
    }
}
