using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest{

    // for later public enum QuestStatus {INACTIVE,ACTIVE,COMPLETE}

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }

    public Quest() { }
}
