using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity.Example;

/* The Event Handler will call and collect game objects
 * to run story type events. Such as command Yarn events, enable/disable npcs,
 * Change NPC locations, add in game logic
*/
public class EventHandler : MonoBehaviour {

    //Collection Objects
    //Children index is based of position in unity object
    public GameObject NPCs_World;
    public GameObject NPCs_Class;
    public GameObject NPCs_spawns;

    //public GameObject chat;
    public GameObject clarice;
    public GameObject jean;
    public GameObject studyBuddy;
    public BoxCollider2D houseExit;
    public Sprite clariceRight;
    public Sprite jeanRight;
    public bool studyEvent = true;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Controls for story type events

        //General Section for time based events
        UpdateForTime();

        //General Section for quest based events
        UpdateForQuest();
	}

    void UpdateForTime()
    {
        /*Quest 0 - Spawn times for chat
        if(!chat.activeSelf && TimeLogic.hour >= 1 && TimeLogic.hour < 5)
        {
            chat.SetActive(true);
        }

        if (chat.activeSelf && TimeLogic.hour > 4)
        {
            chat.SetActive(false);
        }*/

        /*  General logic for when school is out
         *  For NPCs that are in class normally
         */ 
        //Out of class time
        if (TimeLogic.weekDay >= 6 || !(TimeLogic.hour >= 8 && TimeLogic.hour <= 16))
        {
            NPCs_World.SetActive(true);
            //NPCs out of class
            MarketPlace_TimeLogic();
            

        } else
        //Class time
        {
            NPCs_World.SetActive(false);
        }

        if (TimeLogic.weekDay == 7 && LessonLogic.poorProgress)
        {
            if (studyEvent)
            {
                //Lock the house
                houseExit.isTrigger = false;

                //Value of highest friendship
                GameObject bestFriend = getBestFriend();
                if (bestFriend == clarice)
                { 
                    clarice.GetComponent<NPC>().talkToNode = "ClariceStudy";
                    clarice.GetComponent<SpriteRenderer>().sprite = clariceRight;
                }
                else
                {
                    jean.GetComponent<NPC>().talkToNode = "JeanStudy";
                    jean.GetComponent<SpriteRenderer>().sprite = jeanRight;
                }

                studyBuddy = bestFriend;
                studyBuddy.transform.position = NPCs_spawns.transform.GetChild(0).position;
                studyEvent = false;
                
            }
        }
    }
    void UpdateForQuest()
    {

    }
    GameObject getBestFriend()
    {
        List<int> friends = new List<int>();

        //Add all friends here - the value of their friendship
        friends.Add(PlayerLogic.clariceFRN);
        friends.Add(PlayerLogic.jeanFRN);

        //Sort list such that it starts at lowest
        friends.Sort();
        //Reverse list such that it starts at highest
        friends.Reverse();
        
        //Get what NPC had the highest value
        if(friends[0] == PlayerLogic.clariceFRN)
        {
            return clarice;
        } else if(friends[0] == PlayerLogic.jeanFRN)
        {
            return jean;
        } else
        {
            //Default best friend
            return clarice;
        }

    }
    void MarketPlace_TimeLogic()
    {
        //Switch between part time NPCs based on the day
        if (TimeLogic.weekDay % 2 == 1)
        {
            //Get children with transform and sets their game object to active or not
            NPCs_World.transform.GetChild(0).gameObject.SetActive(true);
            NPCs_World.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            NPCs_World.transform.GetChild(0).gameObject.SetActive(false);
            NPCs_World.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
