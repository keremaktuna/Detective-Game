using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AC;
using UnityEngine.Video;

public class DetectiveProfile : MonoBehaviour
{
    private DesktopCanvas desktopCanvas;
    private VideoPlayer videoPlayer;
    public GameObject videoImage;

    private void Start()
    {
        videoImage = gameObject.transform.Find("VideoImage").gameObject;
        videoImage.SetActive(false);
        videoPlayer = gameObject.transform.Find("VideoPlayer").gameObject.GetComponent<VideoPlayer>();
        desktopCanvas = gameObject.transform.parent.gameObject.GetComponent<DesktopCanvas>();
    }

    public void FingerprintButton()
    {
        if(!desktopCanvas.isCopying)
            if (LocalVariables.GetBooleanValue(9) == false)
                StartCoroutine(FingerprintCopyProcess());
    }

    IEnumerator FingerprintCopyProcess()
    {
        videoImage.SetActive(true);
        videoPlayer.frame = 1;
        videoPlayer.Play();
        desktopCanvas.isCopying = true;
        LocalVariables.SetBooleanValue(9, true);
        yield return new WaitForSeconds(7.1f);
        desktopCanvas.isCopying = false;
        videoImage.SetActive(false);
    }
}
