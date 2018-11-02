using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Yarn.Unity;

public class PlayerLogic : MonoBehaviour {

    Rigidbody2D rbody;
    Animator anim;
    private float speed = 1.5f;

    public static int clariceFRN = 0;
    public static int amelieFRN = 0;
    public static int jeanFRN = 0;
    public static int claudeFRN = 0;
    public static int margotFRN = 0;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 movement_vector = new Vector2(CrossPlatformInputManager.GetAxisRaw("Horizontal"), CrossPlatformInputManager.GetAxisRaw("Vertical"));
        //Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

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

        rbody.MovePosition(rbody.position + movement_vector * speed * Time.deltaTime);
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
            default:
                Debug.Log("Error: name not found");
                break;
        }
    }
}
