using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    public int drawWidth;

    private RectTransform drawSurfaceRectTransform;
    private Texture2D drawSurfaceTexture;
    private float drawSurfaceWidth;
    private float drawSurfaceHeight;

    private Vector2 localPointerPosition;

    public Texture2D savedImage;
    [SerializeField] TextureImporterType importType;

    public GameObject compareImage;

    // Use this for initialization
    void Start()
    {
        drawSurfaceRectTransform = this.gameObject.GetComponent<RectTransform>();
        drawSurfaceWidth = drawSurfaceRectTransform.rect.width;
        drawSurfaceHeight = drawSurfaceRectTransform.rect.height;
        drawSurfaceTexture = new Texture2D((int)drawSurfaceWidth, (int)drawSurfaceHeight, TextureFormat.RGB24, false);
        this.gameObject.GetComponent<Image>().material.mainTexture = drawSurfaceTexture;

        // Reset all pixels color to transparent
        Color32 resetColor = new Color32(255, 255, 255, 255);
        Color32[] resetColorArray = drawSurfaceTexture.GetPixels32();

        for (int i = 0; i < resetColorArray.Length; i++)
        {
            resetColorArray[i] = resetColor;
        }

        drawSurfaceTexture.SetPixels32(resetColorArray);
        drawSurfaceTexture.Apply();

        //Debug.Log(GetInstanceID() + " - Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            Color drawColor = Color.black;

            if (RectTransformUtility.RectangleContainsScreenPoint(drawSurfaceRectTransform, Input.mousePosition, null))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(drawSurfaceRectTransform, Input.mousePosition, null, out localPointerPosition);
                for (int i = -(drawWidth / 2); i < (drawWidth / 2); i++)
                {
                    for (int j = -(drawWidth / 2); j < (drawWidth / 2); j++)
                    {
                        drawSurfaceTexture.SetPixel((int)(localPointerPosition.x + (drawSurfaceWidth / 2) + i), (int)(localPointerPosition.y + (drawSurfaceHeight / 2) + j), drawColor);
                    }
                }
                drawSurfaceTexture.Apply();
                Debug.Log(drawSurfaceTexture.GetInstanceID() + " - Drawn");
                //Debug.Log(GetInstanceID() + " - Drawn");
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SaveTexture(drawSurfaceTexture);
        }
    }

    private void SaveTexture(Texture2D texture)
    {
        byte[] bytes = texture.EncodeToPNG();
        var dirPath = Application.dataPath + "/RenderOutput";
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        string assetName = ("/R_" + Random.Range(0, 100000) + ".png");
        System.IO.File.WriteAllBytes(dirPath + assetName, bytes);
        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + dirPath);
        Debug.Log(assetName);
        //GetRefferenceOfImage(assetNameAndPath);
        StartCoroutine(GetRefferenceOfImageWithDelay(assetName));
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    private IEnumerator GetRefferenceOfImageWithDelay(string dirPath)
    {
        yield return new WaitForSeconds(5f);
        string path = ("/RenderOutput" + dirPath);
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(Application.dataPath + path);
        Texture2D imageConvert = new Texture2D(1, 1);
        imageConvert.LoadImage(bytes);

        compareImage.GetComponent<ImageCompare>().getImage(imageConvert);
        /*AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        TextureImporter texSettings = AssetImporter.GetAtPath(path) as TextureImporter;
        if (!texSettings)
        {
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            texSettings = AssetImporter.GetAtPath(path) as TextureImporter;
        }
        texSettings.isReadable = true;
        texSettings.textureType = importType;

        Debug.Log("Done");*/
    }
}
