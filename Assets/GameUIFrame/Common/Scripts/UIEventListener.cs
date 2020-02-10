using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventListener : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerUpHandler,
    ISelectHandler,
    IUpdateSelectedHandler,
    IDeselectHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IScrollHandler,
    IMoveHandler
{
    public delegate void VoidDelegate(GameObject go);

    public VoidDelegate onClick;
    public VoidDelegate onDeSelect;
    public VoidDelegate onDown;
    public VoidDelegate onDrag;
    public VoidDelegate onDragEnd;
    public VoidDelegate onDrop;
    public VoidDelegate onEnter;
    public VoidDelegate onExit;
    public VoidDelegate onMove;
    public VoidDelegate onScroll;
    public VoidDelegate onSelect;
    public VoidDelegate onUp;
    public VoidDelegate onUpdateSelect;

    public object parameter;

    public void OnDeselect(BaseEventData eventData)
    {
        if (onDeSelect != null) onDeSelect(gameObject);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag(gameObject);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null) onDrop(gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onDragEnd != null) onDragEnd(gameObject);
    }

    public void OnMove(AxisEventData eventData)
    {
        if (onMove != null) onMove(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onDown != null) onDown(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onEnter != null) onEnter(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onExit != null) onExit(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onUp != null) onUp(gameObject);
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (onScroll != null) onScroll(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (onSelect != null) onSelect(gameObject);
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (onUpdateSelect != null) onUpdateSelect(gameObject);
    }

    public static UIEventListener Get(GameObject go)
    {
        var listener = go.GetComponent<UIEventListener>();
        if (listener == null) listener = go.AddComponent<UIEventListener>();
        return listener;
    }
}