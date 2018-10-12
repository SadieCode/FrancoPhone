using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.UI;

public class GameControl: MonoBehaviour {

    DictionaryLogic script;
    public GameObject player;
    float xPos;
    float yPos;
    float zPos;
    public GameObject miniMap;

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        xPos = player.transform.position.x;
        yPos = player.transform.position.y;
        zPos = player.transform.position.z;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        data.playerXpos = xPos;
        data.playerYpos = yPos;
        data.playerZpos = zPos;
        data.PlayerDictionarySave = DictionaryLogic.PlayerDictionary;
        data.WordBankSave = DictionaryLogic.WordBank;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(FileExists())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();


            player.transform.position = new Vector3(data.playerXpos, data.playerYpos, data.playerZpos); 
            DictionaryLogic.PlayerDictionary = data.PlayerDictionarySave;
            DictionaryLogic.WordBank = data.WordBankSave;

        }
    }

    public void toggleMap()
    {
        miniMap.SetActive(!miniMap.activeSelf);
    }

    public bool FileExists()
    {
        return File.Exists(Application.persistentDataPath + "/playerInfo.dat");
    }
}

[Serializable]
class PlayerData
{
    //public Vector3 playerPosSave;
    public float playerXpos;
    public float playerYpos;
    public float playerZpos;
    public List<Word> PlayerDictionarySave;
    public List<Word> WordBankSave;
}