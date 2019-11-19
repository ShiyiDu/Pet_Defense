using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public float camSize = 6.75f;
    public Camera[] camGroup;
    public GameObject scroller;

    private float camLength;
    private float scrOffset;
    private float desiredLength = 24f;
    private Vector2 scrInitPos;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCamPos();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateCamPos()
    {
        Debug.Log("screen Length:" + Screen.width);
        Debug.Log("screen Height:" + Screen.height);
        camLength = ((float)Screen.width / Screen.height) * camSize * 2f;
        Debug.Log("CamLength: " + camLength);
        //desiredLength = 16 / 9 * 6.75f * 2; //this line somehow evalutes to 13.5, why???
        Debug.Log("desiredLength: " + desiredLength);

        scrOffset = desiredLength - camLength;
        scrInitPos = scroller.transform.position;
        Debug.Log("screen offset:" + scrOffset);

        Vector2 desiredScrPos = scrInitPos + Vector2.right * scrOffset;
        Vector3 desiredCamPos = Vector3.right * (scrOffset / 2f);
        desiredCamPos.z = -10;

        foreach (Camera cam in camGroup) {
            cam.transform.position = desiredCamPos;
        }

        scroller.transform.position = desiredScrPos;
    }
}
