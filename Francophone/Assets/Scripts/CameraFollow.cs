using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Camera mainCam;
    public Transform TopLeftBound;
    public Transform BottomRightBound;
    float height;
    float width;
    Vector3 newPos;
    Vector3 oldPos;

    // Use this for initialization
    void Start () {
        mainCam = GetComponent<Camera>();
        mainCam.orthographicSize = (Screen.height / 100f) / 4f;
        height = 2f * mainCam.orthographicSize;
        width = height * mainCam.aspect;
        oldPos = this.transform.position;
    }


    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, target.position, 20f * Time.deltaTime) + new Vector3(0, 0, -10);
            if (TopLeftBound == null || BottomRightBound == null)
            {
                
            }
            else
            {
                if (!(newPos.x > TopLeftBound.position.x + (width / 2) && newPos.x < BottomRightBound.position.x - (width / 2)))
                {
                    newPos = new Vector3(oldPos.x, newPos.y, newPos.z);
                }
                if (!(newPos.y < TopLeftBound.position.y - (height / 2) && newPos.y > BottomRightBound.position.y + (height / 2)))
                {
                    newPos = new Vector3(newPos.x, oldPos.y, newPos.z);
                }
            }
            transform.position = newPos;
            oldPos = newPos;
        }
    }

    public void WarpCamera(Transform topLeft, Transform bottomRight)
    {
        TopLeftBound = topLeft;
        BottomRightBound = bottomRight;
        oldPos = target.position;
        if(oldPos.x > BottomRightBound.position.x - (width / 2))
        {
            oldPos = new Vector3(oldPos.x - width/2, oldPos.y, oldPos.z);
        }
        if (oldPos.x < TopLeftBound.position.x + (width / 2))
        {
            oldPos = new Vector3(oldPos.x + width / 2, oldPos.y, oldPos.z);
        }
        if (oldPos.y < BottomRightBound.position.y + (height / 2))
        {
            oldPos = new Vector3(oldPos.x, oldPos.y + height / 2, oldPos.z);
        }
        if (oldPos.y > TopLeftBound.position.y - (height / 2))
        {
            oldPos = new Vector3(oldPos.x, oldPos.y - height / 2, oldPos.z);
        }
    }
}
