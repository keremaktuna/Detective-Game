using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Painting : MonoBehaviour
{
    [Range(1, 100)]
    public int drawWidth;

    private RectTransform drawSurfaceRectTransform;
    private Texture2D drawSurfaceTexture;
    private float drawSurfaceWidth;
    private float drawSurfaceHeight;

    private Vector2 localPointerPosition;

    private GameObject compareImage;

    public Texture2D imageToPaint;

    private bool canPaint = true;

    void Start()
    {
        compareImage = gameObject.transform.parent.gameObject;

        drawSurfaceRectTransform = this.gameObject.GetComponent<RectTransform>();
        drawSurfaceWidth = drawSurfaceRectTransform.rect.width;
        drawSurfaceHeight = drawSurfaceRectTransform.rect.height;
        drawSurfaceTexture = new Texture2D((int)drawSurfaceWidth, (int)drawSurfaceHeight, TextureFormat.RGB24, false);
        this.gameObject.GetComponent<Image>().material.mainTexture = drawSurfaceTexture;

        ResetPainting();
    }

    void Update()
    {
        if(canPaint)
            Drawing(drawSurfaceTexture);
    }

    private void Drawing(Texture2D surfaceTexture)
    {
        if (Input.GetMouseButton(0))
        {
            Color drawColor = Color.black;

            if (RectTransformUtility.RectangleContainsScreenPoint(drawSurfaceRectTransform, Input.mousePosition, null))
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(drawSurfaceRectTransform, Input.mousePosition, null, out localPointerPosition);
                for (int i = -(drawWidth / 2); i < (drawWidth / 2); i++)
                {
                    for (int j = -(drawWidth / 2); j < (drawWidth / 2); j++)
                    {
                        surfaceTexture.SetPixel((int)(localPointerPosition.x + (drawSurfaceWidth / 2) + i), (int)(localPointerPosition.y + (drawSurfaceHeight / 2) + j), drawColor);
                    }
                }
                surfaceTexture.Apply();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            canPaint = false;
            SaveTexture(surfaceTexture);
        }
    }

    private void OnDisable()
    {
        ResetPainting();
        canPaint = true;
    }

    public void ResetPainting()
    {
        Color[] RefferenceImagePixels = imageToPaint.GetPixels();
        Color[] drawSurfaceTextureArray = drawSurfaceTexture.GetPixels();

        for (int i = 0; i < RefferenceImagePixels.Length; i++)
        {
            drawSurfaceTextureArray[i] = RefferenceImagePixels[i];
        }

        drawSurfaceTexture.SetPixels(drawSurfaceTextureArray);
        drawSurfaceTexture.Apply();
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
    }
}
