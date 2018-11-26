using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Yarn.Unity;

public class PlayerLogic : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;
    private float speed = 1.5f;
    public Transform Spawn;
    public GameObject canvas;
    public static int clariceFRN = 0;
    public static int amelieFRN = 0;
    public static int jeanFRN = 0;
    public static int claudeFRN = 0;
    public static int margotFRN = 0;

    Vector2 movement_vector;

    // Use this for initialization
    void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        movement_vector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));

        if(movement_vector != Vector2.zero)
        {
            anim.SetBool("isWalking", true);
            anim.SetFloat("x", movement_vector.x);
            anim.SetFloat("y", movement_vector.y);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
	}

    private void FixedUpdate()
    {
        rbody.MovePosition(rbody.position + movement_vector * speed * Time.fixedDeltaTime);
    }

    [YarnCommand("IncreaseFriendship")]
    public void IncreaseFriendship(string name)
    {
        switch (name)
        {
            case "Clarice":
                clariceFRN += 5;
                break;
            case "Jean":
                jeanFRN += 5;
                break;
            case "Margot":
                margotFRN += 5;
                break;
            case "Claude":
                claudeFRN += 5;
                break;
            case "Amelie":
                amelieFRN += 5;
                break;
            default:
                Debug.Log("Error: name not found");
                break;
        }
    }

    public void Respawn()
    {
        TimeLogic.min = 0;
        TimeLogic.hour = 8;
        TimeLogic.day++;
        transform.position = Spawn.position;
        Camera.main.GetComponent<CameraFollow>().BottomRightBound = null;
        Camera.main.GetComponent<CameraFollow>().TopLeftBound = null;
    }
}
