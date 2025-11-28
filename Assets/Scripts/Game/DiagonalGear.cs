using System.Collections.Generic;
using UnityEngine;

public class DiagonalGear : Gear
{
    public override void SpreadPower()
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
                (current.xpos + 1, current.ypos - 1),
                (current.xpos - 1, current.ypos + 1),
                (current.xpos + 1, current.ypos + 1),
                (current.xpos - 1, current.ypos - 1)
            };

            foreach (var (x, y) in neighbors)
            {
                DropCell cell = gameUI.GetCell(x, y);
                if (cell == null)
                {
                    Debug.Log($"No hay celda en la diagonal ({x},{y})");
                    continue;
                }

                Gear neighbor = cell.GetComponentInChildren<Gear>();
                if (neighbor == null)
                {
                    Debug.Log($"La celda ({x},{y}) existe pero NO tiene Gear");
                    continue;
                }
                if (visited.Contains(neighbor)) continue;

                neighbor.ispowered = true;              // ✅ Encender el engranaje vecino
                neighbor.direction = -current.direction; // ✅ Cambiar sentido

                visited.Add(neighbor);
                queue.Enqueue(neighbor);

            }
        }
    }
}
