using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AC;

public class UsernameHack : MonoBehaviour
{
    public TextAsset nameDataFile, surnameDataFile;
    string[] namePool = new string[1000], surnamePool = new string[1000];

    private TextMeshProUGUI textField;
    string currentText;
    private string characterName, characterSurname;
    string[] currentUsernames = new string[3];
    string mainUsername;
    [HideInInspector] public string selectedUsername;

    private int totalNamesInHack;

    public bool usernameFound = false;
    //[HideInInspector] public bool textSelected = false;
    private GameObject laptopScreen;

    private void Start()
    {
        laptopScreen = gameObject.transform.parent.parent.gameObject;

        textField = GetComponent<TextMeshProUGUI>();

        characterName = LocalVariables.GetStringValue(5).ToLower();
        characterSurname = LocalVariables.GetStringValue(6).ToLower();
        totalNamesInHack = LocalVariables.GetIntegerValue(8);

        GetNamesFromTextFile(nameDataFile, namePool);

        GetNamesFromTextFile(surnameDataFile, surnamePool);

        HackTextGenerator(false);
    }

    private void HackTextGenerator(bool textSelected)
    {
        if (!textSelected)
        {
            currentText = "";
            int truePasswordPlace = Random.Range(0, 3);

            for (int i = 0; i < 3; i++)
            {
                int passwordPlace = Random.Range(0, ((totalNamesInHack / 3) - 1));
                for (int a = 0; a < (totalNamesInHack / 3); a++)
                {
                    if (passwordPlace == a)
                    {
                        if (truePasswordPlace == i)
                        {
                            currentUsernames.SetValue(usernameGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentUsernames[i];
                            mainUsername = rawPassword;
                            rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                        else
                        {
                            currentUsernames.SetValue(usernameGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentUsernames[i];
                            rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                    }
                    else
                    {
                        currentText = currentText + "<link>" + namePool[Random.Range(0, namePool.Length)] + "." + surnamePool[Random.Range(0, surnamePool.Length)] + "</link> ";
                        textField.text = currentText;
                    }
                }
            }
        }

        else
        {
            currentText = "";
            int truePasswordPlace = Random.Range(0, 3);

            for (int i = 0; i < 3; i++)
            {
                int passwordPlace = Random.Range(0, ((totalNamesInHack / 3) - 1));
                for (int a = 0; a < (totalNamesInHack / 3); a++)
                {
                    if (passwordPlace == a)
                    {
                        if (truePasswordPlace == i)
                        {
                            string rawPassword = mainUsername;
                            rawPassword = "<link><color=red>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                        else
                        {
                            currentUsernames.SetValue(usernameGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentUsernames[i];
                            rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                    }
                    else
                    {
                        currentText = currentText + "<link>" + namePool[Random.Range(0, namePool.Length)] + "." + surnamePool[Random.Range(0, surnamePool.Length)] + "</link> ";
                        textField.text = currentText;
                    }
                }
            }
        }
    }

    private string usernameGenerator(int currentPasswordPlace, int truePasswordPlace)
    {
        if (currentPasswordPlace == truePasswordPlace)
        {
            string password = characterName + "." + characterSurname;
            return password;
        }
        else
        {
            string password = characterName + "." + surnamePool[Random.Range(0, surnamePool.Length)];
            //GetRandomYear(true);
            return password;
        }
    }

    private void GetNamesFromTextFile(TextAsset file, string[] array)
    {
        string[] name = file.text.Split('\r', '\n');
        int currentName = 0;

        for (int a = 0; a < name.Length; a++)
        {
            if (name[a] != "")
            {
                array[currentName] = name[a];
                currentName = currentName + 1;
            }
        }
    }

    public void GetClickData(string selectedString)
    {
        if (selectedString == mainUsername)
        {
            usernameFound = true;
            //textField.text = selectedString;
            laptopScreen.GetComponent<LaptopHackScreen>().GetHackedUsername(selectedString);
        }
        else
        {
            usernameFound = false;
            HackTextGenerator(true);
        }
    }
}
