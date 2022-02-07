using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;

public class MailScreen : MonoBehaviour
{
    public GameObject mailScreen;
    public ActionListAsset acList;

    public void OpenMailScreen(GameObject obj)
    {
        Instantiate(mailScreen, gameObject.transform.parent);
        Destroy(obj);
    }

    public void DestroyGameobject(GameObject obj)
    {
        acList.Interact();
        Destroy(obj);
    }
}
