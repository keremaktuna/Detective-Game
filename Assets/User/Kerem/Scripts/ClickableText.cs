using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    string selectedPassword;

    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if(linkIndex > -1)
            {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                var linkID = linkInfo.GetLinkText();

                selectedPassword = linkID;

                gameObject.GetComponent<TestHack>().selectedPassword = linkID;
            }
        }
        if(selectedPassword != null)
        {
            gameObject.GetComponent<TestHack>().choseMainPassword();
            selectedPassword = null;
        }
    }
}
