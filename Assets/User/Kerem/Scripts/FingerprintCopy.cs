using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerprintCopy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject hackText;
    private DesktopCanvas desktopCanvas;

    private void Start()
    {
        desktopCanvas = gameObject.transform.parent.parent.gameObject.GetComponent<DesktopCanvas>();
        hackText = gameObject.transform.Find("HackText").gameObject;
        hackText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!desktopCanvas.isCopying)
            hackText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hackText.SetActive(false);
    }
}
