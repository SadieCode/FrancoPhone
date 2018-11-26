using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatMap : MonoBehaviour {

    public GameObject canvas;
    public Camera mainCam;
    public GameObject Player;
    public Vector3 PlayerLastPosition;

	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        PlayerLastPosition = Player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CombatController.CombatActive) { return; }
        if (collision.gameObject.GetComponent<PlayerLogic>() != null)
        {
            if (Vector3.Distance(collision.gameObject.transform.position, PlayerLastPosition) > 1.0f)
            {
                PlayerLastPosition = collision.gameObject.transform.position;
                Debug.Log(".");
                UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
                int i = UnityEngine.Random.Range(0, 8);
                if (i == 0)
                {
                    TimeLogic.stop = true;
                    canvas.SetActive(false);
                    mainCam.enabled = false;
                    SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
                    CombatController.CombatActive = true;
                }
            }
        }
    }

    public void ResetMap()
    {
        TimeLogic.stop = false;
        canvas.SetActive(true);
        mainCam.enabled = true;
    }
}
