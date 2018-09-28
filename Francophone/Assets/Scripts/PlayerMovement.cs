using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public Transform target;
    Vector2 targetPosition;
    float speed = 6f;


    // Use this for initialization
    void Start () {
        targetPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            targetPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.position = targetPosition;
        }
        if ((Vector2) transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

	}
}
