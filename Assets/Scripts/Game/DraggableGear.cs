using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGear : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _canvas;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _originalPosition;
    private Transform _originalParent;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = _rectTransform.position;
        _originalParent = transform.parent;

        Gear.Instance.BeginDrag(this);
        _canvasGroup.blocksRaycasts = false; // Permite detectar colisiones en el drop
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_canvas == null) return;
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _originalParent)
        {
            _rectTransform.position = _originalPosition; // vuelve a su sitio si no se suelta en una celda
        }

        Gear.Instance.EndDrag();
    }
}
