using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopCanvas : MonoBehaviour
{
    private LaptopHackScreen laptop;

    public GameObject profilePrefab;

    public bool isCopying = false;
    
    private void Start()
    {
        laptop = gameObject.transform.parent.gameObject.GetComponent<LaptopHackScreen>();
    }

    public void ProfileButton()
    {
        if(!isCopying)
            Instantiate(profilePrefab, gameObject.transform);
    }
}
