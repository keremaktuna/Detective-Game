using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaptopHackScreen : MonoBehaviour
{
    public GameObject hackUsernameButton, hackPasswordButton, loginScreen, usernameHackScreen, passwordHackScreen;
    public GameObject passwordInputField, usernameInputField;
    public GameObject passwordHackButton, usernameHackButton;

    private bool isUsernameHacked, isPasswordHacked;

    private void Start()
    {
        hackUsernameButton.SetActive(false);
        hackPasswordButton.SetActive(false);
    }

    public void GetHackedPassword(string password)
    {
        GetHackedText(passwordHackScreen, passwordInputField, password);
        isPasswordHacked = true;
    }

    public void GetHackedUsername(string username)
    {
        GetHackedText(usernameHackScreen, usernameInputField ,username);
        isUsernameHacked = true;
    }

    public void EnablePasswordButtonOnPress(GameObject currentButton)
    {
        if(!isPasswordHacked)
            currentButton.SetActive(true);
    }

    public void EnableUsernameButtonOnPress(GameObject currentButton)
    {
        if (!isUsernameHacked)
            currentButton.SetActive(true);
    }

    public void CloseButton(GameObject currentButton)
    {
        StartCoroutine(CloseButtonWithDelay(currentButton));
    }

    public IEnumerator CloseButtonWithDelay(GameObject currentButton)
    {
        yield return new WaitForSeconds(3f);
        currentButton.SetActive(false);
    }

    private void GetHackedText(GameObject hackScreen, GameObject inputField, string text)
    {
        hackScreen.SetActive(false);
        loginScreen.SetActive(true);
        passwordHackButton.SetActive(false);
        usernameHackButton.SetActive(false);
        inputField.GetComponent<TMP_InputField>().text = text;
        inputField.GetComponent<TMP_InputField>().interactable = false;
    }
}
