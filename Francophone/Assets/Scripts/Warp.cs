/*Written by Mercedez Shuttleworth
  Modified from example by rm2kdev (Youtube)*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    /* 
       Add this to any empty gameobject with a 2d box collider to create a warp exit.
       Make sure the collider is set to trigger or else it won't work.
       Create a corresponding empty game object to be the target location for the warp (a warp entrance).
       The target does not need a collider or script attached to it.
       Make sure no warp entrance is touching any warp exit or your player will loop around (possibly forever).
    */

    //set the target location in the editor
    public Transform target;
    public CameraFollow mainCam;
    public Transform TopLeftBound;
    public Transform BottomRightBound;

    //Turn on panel to hide game screen during warp, this also disables access to movement UI
    public GameObject warpPanel;

    //activate warp when player enters the collision area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //safety check to ensure that what entered the collision area is actually the player
        if(collision.gameObject.GetComponent<PlayerLogic>() != null)
        {
            //hide screen
            warpPanel.SetActive(true);

            //if the collision is the player, move them to target
            collision.gameObject.transform.position = target.position;
            //move the camera too
            mainCam.WarpCamera(TopLeftBound,BottomRightBound);
            //Display screen in 1 second
            Invoke("CloseWarpPanel", 1.0f);
        }
    }

    private void CloseWarpPanel()
    {
        warpPanel.SetActive(false);
    }


}
