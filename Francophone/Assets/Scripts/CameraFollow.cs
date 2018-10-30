using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    Camera mainCam;

	// Use this for initialization
	void Start () {
       mainCam = GetComponent<Camera>();
	}


    private void FixedUpdate()
    {
        mainCam.orthographicSize = (Screen.height / 100f) / 4f;

        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 20f * Time.deltaTime) + new Vector3(0, 0, -10);
        }
    }
}
