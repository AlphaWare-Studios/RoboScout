using UnityEngine.EventSystems;

public class ToolbarManager : EventTrigger
{
    public bool dragging;
    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}
