using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlaceonTouch : MonoBehaviour
{

    public GameObject player;
    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    Instantiate(player, hitPose.position, hitPose.rotation);
                    StartCoroutine(CaptureImageAfterFrames(10));
                }
            }
        }
    }

    private IEnumerator CaptureImageAfterFrames(int frameDelay)
    {
        yield return new WaitForEndOfFrame();
        for(int i = 0; i < frameDelay; i++)
        {
            yield return null;
        }

        CaptureImage();
    }

    void CaptureImage()
    {
        var texture = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] bytes = texture.EncodeToPNG();
        string path = $"{Application.persistentDataPath}/CapturedImage.png";
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log($"Image saved to: {path}");
        Destroy(texture);
    }
}
