using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeatTrigger : MonoBehaviour {

    public LessonLogic lessonMgr;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lessonMgr.readyForLesson) { lessonMgr.StartLesson(); }
    }
}
