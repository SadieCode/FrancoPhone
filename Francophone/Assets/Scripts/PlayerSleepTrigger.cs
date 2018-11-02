using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepTrigger : MonoBehaviour {

    public TimeLogic timeMgr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        timeMgr.Sleep();
    }
}
