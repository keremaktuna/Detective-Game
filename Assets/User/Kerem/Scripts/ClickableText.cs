using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    string selectedText;
    private PasswordHack passwordHackScript;
    private UsernameHack usernameHackScript;
    
    private void Start()
    {
        if(gameObject.GetComponent<PasswordHack>() != null)
            passwordHackScript = gameObject.GetComponent<PasswordHack>();
        if (gameObject.GetComponent<UsernameHack>() != null)
            usernameHackScript = gameObject.GetComponent<UsernameHack>();
    }
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

                selectedText = linkID;
                
                if(passwordHackScript != null)
                {
                    passwordHackScript.selectedPassword = linkID;
                    passwordHackScript.GetClickData(linkID);
                }
                else if(usernameHackScript != null)
                {
                    usernameHackScript.selectedUsername = linkID;
                    usernameHackScript.GetClickData(linkID);
                }
            }
        }
    }
}
