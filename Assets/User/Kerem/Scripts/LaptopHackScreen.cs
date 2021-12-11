using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AC;

public class LaptopHackScreen : MonoBehaviour
{
    private GameObject loginScreen, passwordHackButton, usernameHackButton, loginButton;
    private GameObject usernameHackScreen, passwordHackScreen;
    private GameObject passwordInputField, usernameInputField;
    private GameObject desktopCanvas, background;

    public ActionListAsset list;

    private bool usernameHacked, passwordHacked;

    private void Start()
    {
        GetVariables();
    }

    public void GetHackedPassword(string password)
    {
        GetHackedText(passwordHackScreen, passwordInputField, password);
        LocalVariables.SetBooleanValue(0, true);
        passwordHacked = true;

        if (usernameHacked)
            loginButton.SetActive(true);
    }

    public void GetHackedUsername(string username)
    {
        GetHackedText(usernameHackScreen, usernameInputField ,username);
        LocalVariables.SetBooleanValue(1, true);
        usernameHacked = true;

        if (passwordHacked)
            loginButton.SetActive(true);
    }

    public void OpenDesktop()
    {
        background.SetActive(false);
        loginScreen.SetActive(false);
        passwordHackScreen.SetActive(false);
        usernameHackScreen.SetActive(false);
        desktopCanvas.SetActive(true);
    }

    public void EnablePasswordButtonOnPress(GameObject currentButton)
    {
        if(!passwordHacked)
            currentButton.SetActive(true);
    }

    public void EnableUsernameButtonOnPress(GameObject currentButton)
    {
        if (!usernameHacked)
            currentButton.SetActive(true);
    }

    public void CloseButton(GameObject currentButton)
    {
        StartCoroutine(CloseButtonWithDelay(currentButton));
    }

    public void FingerprintCopy()
    {
        LocalVariables.SetBooleanValue(9, true);
    }

    public IEnumerator CloseButtonWithDelay(GameObject currentButton)
    {
        yield return new WaitForSeconds(3f);
        currentButton.SetActive(false);
    }

    public void DestroyGameObject()
    {
        LocalVariables.SetBooleanValue(2, false);
        list.Interact();
        Destroy(gameObject);
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

    private void GetVariables()
    {
        usernameHacked = LocalVariables.GetBooleanValue(1);
        passwordHacked = LocalVariables.GetBooleanValue(0);

        loginScreen = gameObject.transform.Find("LoginScreen").gameObject;
        usernameHackButton = loginScreen.transform.Find("UsernameHackButton").gameObject;
        passwordHackButton = loginScreen.transform.Find("PasswordHackButton").gameObject;
        usernameHackScreen = gameObject.transform.Find("UsernameHackCanvas").gameObject;
        passwordHackScreen = gameObject.transform.Find("PasswordHackCanvas").gameObject;
        loginButton = loginScreen.transform.Find("LoginButton").gameObject;
        passwordInputField = loginScreen.transform.Find("PasswordField").gameObject;
        usernameInputField = loginScreen.transform.Find("UsernameField").gameObject;
        desktopCanvas = gameObject.transform.Find("DesktopCanvas").gameObject;
        background = gameObject.transform.Find("Background").gameObject;

        usernameHackButton.SetActive(false);
        passwordHackButton.SetActive(false);
        usernameHackScreen.SetActive(false);
        passwordHackScreen.SetActive(false);
        loginButton.SetActive(false);
        desktopCanvas.SetActive(false);

        if (usernameHacked)
            IsHacked(usernameInputField, LocalVariables.GetStringValue(4));

        if (passwordHacked)
            IsHacked(passwordInputField, LocalVariables.GetStringValue(3));

        if (usernameHacked && passwordHacked)
            loginButton.SetActive(true);
    }

    private void IsHacked(GameObject inputField, string text)
    {
        passwordHackButton.SetActive(false);
        usernameHackButton.SetActive(false);
        inputField.GetComponent<TMP_InputField>().text = text;
        inputField.GetComponent<TMP_InputField>().interactable = false;
    }
}
