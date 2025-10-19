using UnityEngine;
using UnityEngine.EventSystems;

public class Gear : MonoBehaviour
{
    public static Gear Instance { get; private set; }

    public DraggableGear currentDraggedItem;

    private void Awake()
    {
        // Patrón Singleton básico
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void BeginDrag(DraggableGear item)
    {
        currentDraggedItem = item;
    }

    public void EndDrag()
    {
        currentDraggedItem = null;
    }
}
