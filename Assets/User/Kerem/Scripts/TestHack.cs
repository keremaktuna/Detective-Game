using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestHack : MonoBehaviour
{
    private string charString = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz1234567890.,!?*";
    private char[] chars;

    TextMeshProUGUI textField;
    string currentText;
    public string characterName = "";
    string[] currentPasswords = new string[3];
    int passwordLength;
    string mainPassword;
    [HideInInspector] public string selectedPassword;
    bool passwordFirstTry = true;

    public int totalLengthOfHack;

    bool passwordFound;

    void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        passwordLength = characterName.Length + Random.Range(3, 6);
        chars = charString.ToCharArray();

        FirstHackTextGenerator();
    }

    private void FirstHackTextGenerator()
    {
        currentText = "";

        for (int i = 0; i < 3; i++)
        {
            int passwordPlace = Random.Range(0, ((totalLengthOfHack / 3) - 1));
            for (int a = 0; a < totalLengthOfHack / 3; a++)
            {
                if (passwordPlace == a)
                {
                    currentPasswords.SetValue(passwordGenerator(), i);
                    string rawPassword = currentPasswords[i];
                    rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                    currentText = currentText + rawPassword;
                    a = a + currentPasswords[i].Length - 1;
                }
                else
                {
                    currentText = currentText + chars[Random.Range(0, chars.Length)];
                    textField.text = currentText;
                }
            }
        }
    }

    public void HackTextGenerator()
    {
        currentText = "";

        bool mainPasswordUsed = false;

        for (int i = 0; i < 3; i++)
        {
            int passwordPlace = Random.Range(0, ((totalLengthOfHack / 3) - 1));
            for (int a = 0; a < totalLengthOfHack / 3; a++)
            {
                if (passwordPlace == a)
                {
                    if (mainPasswordUsed)
                    {
                        currentPasswords.SetValue(passwordGenerator(), i);
                        string rawPassword = currentPasswords[i];
                        rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                        currentText = currentText + rawPassword;
                        a = a + currentPasswords[i].Length - 1;
                    }
                    else if (!mainPasswordUsed)
                    {
                        string rawPassword = mainPassword;
                        currentPasswords.SetValue(mainPassword, i);
                        rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                        currentText = currentText + rawPassword;
                        a = a + currentPasswords[i].Length - 1;
                        mainPasswordUsed = true;
                    }
                }
                else
                {
                    currentText = currentText + chars[Random.Range(0, chars.Length)];
                    textField.text = currentText;
                }
            }
        }
    }
    private string passwordGenerator()
    {
        string password = characterName;

        for (int x = 0; x < (passwordLength - characterName.Length); x++)
        {
            password = password + chars[Random.Range(0, chars.Length)];
        }

        return password;
    }


    public void choseMainPassword()
    {
        if(passwordFirstTry)
        {
            int selectedPasswordIndex = System.Array.IndexOf(currentPasswords, selectedPassword);
            string[] remainingPassword = new string[2];
            if (selectedPasswordIndex == 0)
                PasswordSearch(1, 2, remainingPassword);

            else if (selectedPasswordIndex == 1)
                PasswordSearch(0, 2, remainingPassword);

            else if (selectedPasswordIndex == 2)
                PasswordSearch(0, 1, remainingPassword);

            passwordFirstTry = false;

            HackTextGenerator();
        }
        else
        {
            if(mainPassword == selectedPassword)
            {
                passwordFound = true;
            }
            else
            {
                HackTextGenerator();
            }
        }
    }
    private void PasswordSearch(int a, int b, string[] x)
    {
        x.SetValue(currentPasswords[1], 0);
        x.SetValue(currentPasswords[2], 1);
        mainPassword = x[Random.Range(0, x.Length)];
    }
}
