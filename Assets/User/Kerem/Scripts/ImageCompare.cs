using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCompare : MonoBehaviour
{
    public float similarityPercent;
    public Texture2D first, second;
    [HideInInspector] public Texture2D drawnImage;
    private float imageWithoutWhite;

    [SerializeField] bool testFirst, testSecond;

    private PhoneHackScreen phoneHackScreen;

    private void Start()
    {
        phoneHackScreen = gameObject.transform.parent.gameObject.GetComponent<PhoneHackScreen>();

        Color[] firstPic = first.GetPixels();
        for (int a = 0; a < firstPic.Length; a++)
        {
            if (firstPic[a] == Color.black)
            {
                imageWithoutWhite++;
            }
        }
    }

    private void Update()
    {
        if (testFirst)
        {
            Compare(first, second);
            Debug.Log(similarityPercent);
            testFirst = false;
        }

        if (testSecond)
        {
            Compare(first, drawnImage);
            Debug.Log(similarityPercent);
            testSecond = false;
        }
    }

    private void Compare(Texture2D first, Texture2D second)
    {
        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();

        similarityPercent = 0;

        for(int i = 0; i < firstPix.Length; i++)
        {
            if (firstPix[i] == Color.black)
            {
                if (firstPix[i] == secondPix[i])
                {
                    similarityPercent++;
                }
            }

        }

        similarityPercent = (similarityPercent * 100) / imageWithoutWhite; 

        phoneHackScreen.GetImageCompareResult(similarityPercent);
    }

    public void getImage(Texture2D imageToGet)
    {
        drawnImage = imageToGet;
        Compare(first, drawnImage);
        Debug.Log(similarityPercent);
    }

    public void CloseButton()
    {
        phoneHackScreen.ClosePaintingCanvas();
        gameObject.SetActive(false);
    }
}
