using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour {

    public static int health = 100;
    public static int maxHealth = 100;
    public static int attackDamage = 20;
    public bool dead = false;
    public Enemy enemy;
    public Text TxtHealth;
    Animator Anim;

    // Use this for initialization
    void Start () {
        Anim = GetComponent<Animator>();
        TxtHealth.text = health.ToString() + "/" + maxHealth;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack()
    {
        Anim.SetTrigger("Attack");
        int d = (int) Math.Round(attackDamage * (SpellCaster.powerLevel/100));
        enemy.TakeDamage(d);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        TxtHealth.text = health.ToString() + "/" + maxHealth;
        if (health <= 0)
        {
            health = 0;
            TxtHealth.text = health.ToString() + "/" + maxHealth;
            Death();
        }

    }

    public void Death()
    {
        Anim.SetTrigger("Dead");
        dead = true;
    }
}
