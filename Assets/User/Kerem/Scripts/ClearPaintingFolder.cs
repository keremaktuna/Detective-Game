using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using AC;

public class ClearPaintingFolder : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        FileUtil.DeleteFileOrDirectory(Application.dataPath + "/RenderOutput");
    }
}
