using UnityEngine;
using UnityEngine.EventSystems;

public class SliderEnd : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public bool isSliding;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSliding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSliding = false;
    }
}