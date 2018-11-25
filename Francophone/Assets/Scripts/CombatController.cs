using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatController : MonoBehaviour {

    public GameObject btnAttack;
    public SpellCaster SpellCaster;
    public PlayerCombat Player;
    public PlayerLogic PlayerLogic;
    public Enemy Enemy;
    public CombatMap combatMap;
    public static bool CombatActive = false;
    bool playerTurn = true;
    bool enemyTurn = false;
    public static bool attackReady = false;

	// Use this for initialization
	void Start () {
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        combatMap = GameObject.Find("Forest").GetComponent<CombatMap>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Player.dead)
        {
            playerTurn = false;
            enemyTurn = false;
            BattleLost();

        }

        if (Enemy.dead)
        {
            playerTurn = false;
            enemyTurn = false;
            BattleWon();
        }

        if (playerTurn && attackReady && !Player.dead)
        {
            Player.Attack();
            playerTurn = false;
            attackReady = false;
            enemyTurn = true;
        }

        if (enemyTurn && !Enemy.dead)
        {
            Enemy.Attack();
            enemyTurn = false;
            playerTurn = true;
            btnAttack.SetActive(true);
        }
	}

    public void BtnAttack()
    {
        btnAttack.SetActive(false);
        SpellCaster.DisplayQuestion();
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
}
