using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AC;

public class LaptopHackScreen : MonoBehaviour
{
    public GameObject loginScreen, passwordHackButton, usernameHackButton, loginButton;
    public GameObject usernameHackScreen, passwordHackScreen;
    public GameObject passwordInputField, usernameInputField;
    public GameObject desktopCanvas, background;
    private GameObject exitButton;

    public GameObject loginScreenPrefab, desktopCanvasPrefab, usernameHackScreenPrefab, passwordHackScreenPrefab;

    public ActionListAsset list;

    private bool usernameHacked, passwordHacked;

    private void Start()
    {
        background = gameObject.transform.Find("Background").gameObject;
        loginScreen = Instantiate(loginScreenPrefab, gameObject.transform);
        exitButton = gameObject.transform.Find("ExitButton").gameObject;
        exitButton.transform.SetAsLastSibling();

        usernameHacked = LocalVariables.GetBooleanValue(1);
        passwordHacked = LocalVariables.GetBooleanValue(0);
    }

    public void GetHackedPassword(string password)
    {
        //GetHackedText(passwordHackScreen, passwordInputField, password);
        LocalVariables.SetBooleanValue(0, true);
        passwordHacked = true;

        Destroy(passwordHackScreen);
        loginScreen = Instantiate(loginScreenPrefab, gameObject.transform);
        exitButton.transform.SetAsLastSibling();


        loginScreen.GetComponent<LoginScreen>().HackedPassword(password);

        if (usernameHacked)
            loginButton.SetActive(true);
    }

    public void GetHackedUsername(string username)
    {
        //GetHackedText(usernameHackScreen, usernameInputField ,username);
        LocalVariables.SetBooleanValue(1, true);
        usernameHacked = true;

        Destroy(usernameHackScreen);
        loginScreen = Instantiate(loginScreenPrefab, gameObject.transform);
        exitButton.transform.SetAsLastSibling();


        loginScreen.GetComponent<LoginScreen>().HackedUsername(username);

        if (passwordHacked)
            loginButton.SetActive(true);
    }

    public void OpenDesktop()
    {
        background.SetActive(false);
        Destroy(loginScreen);
        Instantiate(desktopCanvasPrefab, gameObject.transform);
        exitButton.transform.SetAsLastSibling();
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
        if(currentButton != null)
            currentButton.SetActive(false);
    }

    public void DestroyGameObject()
    {
        LocalVariables.SetBooleanValue(2, false);
        list.Interact();
        Destroy(gameObject);
    }

    public void UsernameHackButton()
    {
        Destroy(loginScreen);
        Instantiate(usernameHackScreenPrefab, gameObject.transform);
        exitButton.transform.SetAsLastSibling();

    }

    public void PasswordHackButton()
    {
        Destroy(loginScreen);
        Instantiate(passwordHackScreenPrefab, gameObject.transform);
    }
}
