using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class Quest{

    // for later public enum QuestStatus {INACTIVE,ACTIVE,COMPLETE}

    //ID acts as key for accessing and updating quests
    public int Id { get; set; }

    //Quest name
    public string Name { get; set; }

    //Quest description (gives an overview of quest, does not change with progress)
    public string Description { get; set; }

    //List of all objectives in the quest
    [XmlArray("Objectives")]
    [XmlArrayItem("Objective")]
    public List<Objective> Objectives { get; set; }

    //Status 0 = INACTIVE (user has not interacted with the quest)
    //Status 1 = ACTIVE (user is currently doing the quest)
    //Status 2 = COMPLETE (user has finished all objectives in the quest)
    public int Status { get; set; }

    //Current objective of quest that user is to do
    public int CurrentObjective { get; set; }

    //default constructor
    public Quest() { }

    public void NextObjective()
    {
        if(CurrentObjective + 1 < Objectives.Count)
        {
            CurrentObjective++;
        }
        else
        {
            CompleteQuest();
        }
    }

    public void CompleteQuest()
    {
        Status = 2;
    }
}
