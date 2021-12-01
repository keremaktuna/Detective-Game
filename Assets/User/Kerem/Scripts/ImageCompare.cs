using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCompare : MonoBehaviour
{
    float similarityPercent;
    public Texture2D first, second;
    float imagePixelSize;

    private void Start()
    {
        imagePixelSize = first.GetPixels().Length;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Compare(first, second);
            Debug.Log(similarityPercent);
        }
    }

    private void Compare(Texture2D first, Texture2D second)
    {
        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();

        similarityPercent = 0;

        for(int i = 0; i < firstPix.Length; i++)
        {
            if(firstPix[i] == secondPix[i])
            {
                similarityPercent++;
            }
        }

        similarityPercent = (similarityPercent * 100) / imagePixelSize; 
    }
}
