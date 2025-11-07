using System;
using System.Collections.Generic;
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
    public float rotationSpeed = 10;
    public bool ispowered = false;
    public int direction = 1;
    public int xpos;
    public int ypos;
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnGearclick);
        gameUI = FindFirstObjectByType<GameUI>();
    }
    private void OnGearclick()
    {
        gameUI.SelectGear(this);
        ispowered = false;
    }
    private void FixedUpdate()
    {
        if (ispowered && isOnDropcell)
        {
            transform.Rotate(0f, 0f, rotationSpeed * direction * Time.fixedDeltaTime);
        }
    }
    public void CheckPosition()
    {
        DropCell poscheck = GetComponentInParent<DropCell>();
        if (poscheck != null)
        {
            xpos = poscheck.slotX;
            ypos = poscheck.slotY;
            isOnDropcell = true;
        }
        else
        {
            isOnDropcell = false;
        }
    }
    public void SpreadPower()
    {
        if (!isOnDropcell) return;

        Queue<Gear> queue = new Queue<Gear>();
        HashSet<Gear> visited = new HashSet<Gear>();

        queue.Enqueue(this);
        visited.Add(this);

        int bottomRow = gameUI.height - 1;

        while (queue.Count > 0)
        {
            Gear current = queue.Dequeue();

            // DEBUG: ver coordenadas en consola (quita luego)
            Debug.Log($"Spread: gear at x={current.xpos}, y={current.ypos}, bottomRow={bottomRow}");

            if (current.ypos == bottomRow)
            {
                GearToPower target = gameUI.GetUnPoweredGearAtColumn(current.xpos);
                if (target != null)
                {
                    target.ispowered = true;
                    target.direction = -current.direction;
                    Debug.Log($"Activated GearToPower at column {current.xpos}");
                }
            }

            (int x, int y)[] neighbors = new (int, int)[]
            {
                (current.xpos, current.ypos + 1),
                (current.xpos, current.ypos - 1),
                (current.xpos - 1, current.ypos),
                (current.xpos + 1, current.ypos)
            };

            foreach (var (x, y) in neighbors)
            {
                DropCell cell = gameUI.GetCell(x, y);
                if (cell == null) continue;

                Gear neighbor = cell.GetComponentInChildren<Gear>();
                if (neighbor == null) continue;
                if (visited.Contains(neighbor)) continue;

                neighbor.ispowered = true;              // ✅ Encender el engranaje vecino
                neighbor.direction = -current.direction; // ✅ Cambiar sentido

                visited.Add(neighbor);
                queue.Enqueue(neighbor);

            }
        }
    }

}
