using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInfo : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{

    public GameObject infoObj;

    void Start()
    {
        Hide();
    }

    public void Show()
    {
        infoObj.SetActive(true);
    }

    public void Hide()
    {
        infoObj.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Show();
        Debug.Log("OnPointerEnter call by " + name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hide();
        Debug.Log("OnPointerExit call by" + name);
    }
}