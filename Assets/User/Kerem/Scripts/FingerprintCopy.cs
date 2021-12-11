using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerprintCopy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private GameObject hackText;

    private int imageCompareResult;

    private void Start()
    {
        hackText = gameObject.transform.Find("Hack").gameObject;
        hackText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hackText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hackText.SetActive(false);
    }
}
