using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCell : MonoBehaviour
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableGear draggedItem = Gear.Instance.currentDraggedItem;

        if (draggedItem != null)
        {
            draggedItem.transform.SetParent(transform);
            draggedItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
