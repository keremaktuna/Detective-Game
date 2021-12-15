using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameHackCanvas : MonoBehaviour
{
    private LaptopHackScreen laptop;

    private void Awake()
    {
        laptop = gameObject.transform.parent.gameObject.GetComponent<LaptopHackScreen>();

        laptop.usernameHackScreen = gameObject;
    }
}
