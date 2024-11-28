using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableContentHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] bool isDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public bool IsDragging()
    {
        return isDragging;
    }
}
