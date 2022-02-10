using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlarmButton : MonoBehaviour
{
    public Image image;

    public Sprite onButton, offButton;
    bool isButtonActive = false;

    public void ButtonPress()
    {
        if(isButtonActive)
        {
            isButtonActive = false;
            image.sprite = offButton;
        }
        else if (!isButtonActive)
        {
            isButtonActive = true;
            image.sprite = onButton;
        }
    }
}
