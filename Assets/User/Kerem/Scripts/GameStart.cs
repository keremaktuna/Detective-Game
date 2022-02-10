using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

public class GameStart : MonoBehaviour
{
    public ActionListAsset acList;

    void Start()
    {
        acList.Interact();
    }
}
