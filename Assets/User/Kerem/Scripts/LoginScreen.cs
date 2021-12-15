using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AC;

public class LoginScreen : MonoBehaviour
{
    private LaptopHackScreen laptop;

    private GameObject passwordHackButton, usernameHackButton, loginButton, passwordInputField, usernameInputField;

    private void Awake()
    {
        laptop = gameObject.transform.parent.gameObject.GetComponent<LaptopHackScreen>();

        laptop.loginScreen = gameObject;
        passwordHackButton = gameObject.transform.GetChild(5).gameObject;
        laptop.passwordHackButton = passwordHackButton;
        usernameHackButton = gameObject.transform.GetChild(3).gameObject;
        laptop.usernameHackButton = usernameHackButton;
        loginButton = gameObject.transform.GetChild(6).gameObject;
        laptop.loginButton = loginButton;
        passwordInputField = gameObject.transform.GetChild(4).gameObject;
        laptop.passwordInputField = passwordInputField;
        usernameInputField = gameObject.transform.GetChild(2).gameObject;
        laptop.usernameInputField = usernameInputField;

        usernameHackButton.SetActive(false);
        passwordHackButton.SetActive(false);
        loginButton.SetActive(false);

        CheckPasswordAndUsername();
    }

    public void HackedPassword(string password)
    {
        passwordInputField.GetComponent<TMP_InputField>().text = password;
        passwordInputField.GetComponent<TMP_InputField>().interactable = false;

        CheckPasswordAndUsername();
    }

    public void HackedUsername(string username)
    {
        usernameInputField.GetComponent<TMP_InputField>().text = username;
        usernameInputField.GetComponent<TMP_InputField>().interactable = false;

        CheckPasswordAndUsername();
    }

    public void CheckPasswordAndUsername()
    {
        if (LocalVariables.GetBooleanValue(0))//password
        {
            passwordInputField.GetComponent<TMP_InputField>().text = LocalVariables.GetStringValue(3);
            passwordInputField.GetComponent<TMP_InputField>().interactable = false;

            passwordHackButton.SetActive(false);

        }
        if (LocalVariables.GetBooleanValue(1))//username
        {
            usernameInputField.GetComponent<TMP_InputField>().text = LocalVariables.GetStringValue(4);
            usernameInputField.GetComponent<TMP_InputField>().interactable = false;

            usernameHackButton.SetActive(false);
        }

        if (LocalVariables.GetBooleanValue(0) && LocalVariables.GetBooleanValue(1))
            loginButton.SetActive(true);
    }

    public void PasswordHackButton()
    {
        laptop.PasswordHackButton();
    }

    public void UsernameHackButton()
    {
        laptop.UsernameHackButton();
    }

    public void OpenDesktop()
    {
        laptop.OpenDesktop();
    }

    public void EnablePasswordButtonOnPress(GameObject currentButton)
    {
        laptop.EnablePasswordButtonOnPress(currentButton);
    }

    public void EnableUsernameButtonOnPress(GameObject currentButton)
    {
        laptop.EnableUsernameButtonOnPress(currentButton);
    }

    public void CloseButton(GameObject currentButton)
    {
        laptop.CloseButton(currentButton);
    }
}
