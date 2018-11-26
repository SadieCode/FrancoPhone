using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour {

    public GameObject TransitionPanel;
    public GameObject btnAttack;
    public SpellCaster SpellCaster;
    public PlayerCombat Player;
    public PlayerLogic PlayerLogic;
    public Enemy Enemy;
    public CombatMap combatMap;
    public static bool CombatActive = false;
    public bool playerTurn = true;
    public bool enemyTurn = false;
    public static bool attackReady = false;

	// Use this for initialization
	void Start () {
        Invoke("ClosePanel", 1.5f);
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        combatMap = GameObject.Find("Forest").GetComponent<CombatMap>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Player.dead)
        {
            btnAttack.SetActive(false);
            playerTurn = false;
            enemyTurn = false;
            Invoke("BattleLost", 4.0f);

        }

        if (Enemy.dead)
        {
            playerTurn = false;
            enemyTurn = false;
            Invoke("BattleWon", 4.0f);
        }

        if (playerTurn && attackReady && !Player.dead)
        {
            Player.Attack();
            playerTurn = false;
            Invoke("EnemyTurn", 3.0f);
        }

        if (enemyTurn && !Enemy.dead)
        {
            Enemy.Attack();
            enemyTurn = false;
            Invoke("PlayerTurn", 3.0f);
        }
	}

    public void BtnAttack()
    {
        btnAttack.SetActive(false);
        SpellCaster.DisplayQuestion();
    }

    public void PlayerTurn()
    {
        playerTurn = true;
        btnAttack.SetActive(true);
    }

    public void EnemyTurn()
    {
        attackReady = false;
        enemyTurn = true;
    }

    public void BattleLost()
    {
        combatMap.ResetMap();
        PlayerLogic.Respawn();
        CombatActive = false;
        PlayerCombat.health = 100;
        Player.dead = false;
        SceneManager.UnloadSceneAsync("CombatScene");
    }

    public void BattleWon()
    {
        combatMap.ResetMap();
        CombatActive = false;
        SceneManager.UnloadSceneAsync("CombatScene");
    }

    void ClosePanel()
    {
        TransitionPanel.SetActive(false);
    }
}
