using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCompare : MonoBehaviour
{
    float similarityPercent;
    public Texture2D first, second, white, drawnImage;
    float imagePixelSize, imageWithoutWhite;

    [SerializeField] bool testFirst, testSecond;

    private void Start()
    {
        imagePixelSize = first.GetPixels().Length;

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
    }
    
    public void getImage(Texture2D imageToGet)
    {
        drawnImage = imageToGet;
    }
}
