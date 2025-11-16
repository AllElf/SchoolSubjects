using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonProbe : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public int index;

    public void OnPointerDown(PointerEventData e) { Debug.Log($"[BTN {index}] Down"); }
    public void OnPointerUp(PointerEventData e) { Debug.Log($"[BTN {index}] Up"); }
    public void OnPointerClick(PointerEventData e) { Debug.Log($"[BTN {index}] Click!"); }
}
