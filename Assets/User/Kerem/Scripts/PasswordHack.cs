using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AC;

public class PasswordHack : MonoBehaviour
{
    public TextAsset dataFile;
    string[] namePool = new string[1000];

    private TextMeshProUGUI textField;
    string currentText;
    int detectiveBirthYear;
    private string detectiveName;
    string[] currentPasswords = new string[3];
    string mainPassword;
    [HideInInspector] public string selectedPassword;

    private int totalNamesInHack;

    public bool passwordFound = false;
    private GameObject laptopScreen;

    private void Start()
    {
        laptopScreen = gameObject.transform.parent.parent.gameObject;

        textField = GetComponent<TextMeshProUGUI>();

        detectiveBirthYear = LocalVariables.GetIntegerValue(7);
        detectiveName = LocalVariables.GetStringValue(5);
        totalNamesInHack = LocalVariables.GetIntegerValue(8);

        GetNamesFromTextFile(dataFile, namePool);

        HackTextGenerator(false);
    }

    private void HackTextGenerator(bool textSelected)
    {
        if(!textSelected)
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
                            currentPasswords.SetValue(passwordGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentPasswords[i];
                            mainPassword = rawPassword;
                            rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                        else
                        {
                            currentPasswords.SetValue(passwordGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentPasswords[i];
                            rawPassword = "<link><color=green>" + rawPassword + GetRandomYear(true) + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                    }
                    else
                    {
                        currentText = currentText + "<link>" + namePool[Random.Range(0, namePool.Length)] + GetRandomYear(false) + "</link> ";
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
                            string rawPassword = mainPassword;
                            rawPassword = "<link><color=green>" + rawPassword + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                        else
                        {
                            currentPasswords.SetValue(passwordGenerator(i, truePasswordPlace), i);
                            string rawPassword = currentPasswords[i];
                            rawPassword = "<link><color=green>" + rawPassword + GetRandomYear(true) + "</color></link>";
                            currentText = currentText + rawPassword + " ";
                        }
                    }
                    else
                    {
                        currentText = currentText + "<link>" + namePool[Random.Range(0, namePool.Length)] + GetRandomYear(false) + "</link> ";
                        textField.text = currentText;
                    }
                }
            }
        }
    }

    private string passwordGenerator(int currentPasswordPlace, int truePasswordPlace)
    {
        if (currentPasswordPlace == truePasswordPlace)
        {
            string password = (detectiveName + detectiveBirthYear);
            return password;
        }
        else
        {
            string password = detectiveName;
            GetRandomYear(true);
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

    private int GetRandomYear(bool ifDetective)
    {
        if (ifDetective)
        {
            int year = 0;
            year = (1900 + Random.Range(50, 99));

            if (year == detectiveBirthYear)
            {
                year = +(year + Random.Range(1, 9));
            }
            return year;
        }
        else
        {
            return (1900 + Random.Range(50, 99));
        }
    }

    public void GetClickData(string selectedString)
    {
        if (selectedString == mainPassword)
        {
            passwordFound = true;
            //textField.text = selectedString;
            laptopScreen.GetComponent<LaptopHackScreen>().GetHackedPassword(selectedString);
        }
        else
        {
            passwordFound = false;
            HackTextGenerator(true);
        }
    }
}
