using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

public class PhoneHackScreen : MonoBehaviour
{
    public ActionListAsset list, afterWake;

    private GameObject lockScreen, fingerprintHackScreen, phoneHomeScreen;

    private float imageCompareResult;

    private void Start()
    {
        lockScreen = gameObject.transform.Find("LockScreen").gameObject;
        fingerprintHackScreen = gameObject.transform.Find("PaintingCanvas").gameObject;
        phoneHomeScreen = gameObject.transform.Find("PhoneHomeScreen").gameObject;

        fingerprintHackScreen.SetActive(false);
        phoneHomeScreen.SetActive(false);
    }

    public void SmartHomeButton(GameObject obj)
    {
        afterWake.Interact();
        Destroy(obj);
    }

    public void FingerprintButton()
    {
        if(LocalVariables.GetBooleanValue(9))
        {
            if(LocalVariables.GetBooleanValue(11))
            {
                lockScreen.SetActive(false);
                fingerprintHackScreen.SetActive(true);
            }
            else
            {
                lockScreen.SetActive(false);
                phoneHomeScreen.SetActive(true);
            }
        }
    }

    public void ClosePhone()
    {
        LocalVariables.SetBooleanValue(10, false);
        list.Interact();
        Destroy(gameObject);
    }

    public void ClosePaintingCanvas()
    {
        lockScreen.SetActive(true);
    }

    public void GetImageCompareResult(float number)
    {
        imageCompareResult = number;
        fingerprintHackScreen.SetActive(false);

        if (imageCompareResult < 95)
        {
            fingerprintHackScreen.SetActive(true);
            Debug.Log("Try Again!");
        }
        else
        {
            fingerprintHackScreen.SetActive(false);
            phoneHomeScreen.SetActive(true);
            LocalVariables.SetBooleanValue(11, false);
        }
    }
}
