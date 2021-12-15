using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableUI : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    private RectTransform dragRectTransform;
    private Canvas canvas;

    public void OnDrag(PointerEventData eventData)
    {
        if(dragRectTransform == null)
        {
            dragRectTransform = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
            canvas = gameObject.transform.parent.parent.gameObject.GetComponent<Canvas>();
        }
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(dragRectTransform != null)
            dragRectTransform.SetAsLastSibling();
    }

    public void DestroyGameobject(GameObject obj)
    {
        Destroy(obj);
    }
}
