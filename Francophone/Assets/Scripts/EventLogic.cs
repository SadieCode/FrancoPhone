using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;

public class EventLogic : MonoBehaviour {

    //public GameObject chat;
    public GameObject clarice;
    public GameObject jean;
    public GameObject studyBuddy;
    public BoxCollider2D houseExit;
    public Transform npcSpawn;
    public Sprite clariceRight;
    public Sprite jeanRight;
    public bool studyEvent = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
        /*Quest 0
        if(!chat.activeSelf && TimeLogic.hour >= 1 && TimeLogic.hour < 5)
        {
            chat.SetActive(true);
        }

        if (chat.activeSelf && TimeLogic.hour > 4)
        {
            chat.SetActive(false);
        }*/

        if(TimeLogic.weekDay == 7 && LessonLogic.poorProgress)
        {
            if (studyEvent) {
                houseExit.isTrigger = false;

                if(PlayerLogic.clariceFRN >= PlayerLogic.jeanFRN)
                {
                    studyBuddy = clarice;
                    clarice.GetComponent<NPC>().talkToNode = "ClariceStudy";
                    clarice.GetComponent<SpriteRenderer>().sprite = clariceRight;
                }
                else
                {
                    studyBuddy = jean;
                    jean.GetComponent<NPC>().talkToNode = "JeanStudy";
                    jean.GetComponent<SpriteRenderer>().sprite = jeanRight;
                }

                studyBuddy.transform.position = npcSpawn.position;
                studyEvent = false;
            }
        }
    }
}
