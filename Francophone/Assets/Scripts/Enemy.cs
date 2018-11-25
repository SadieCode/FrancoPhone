using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public int health = 40;
    public int maxHealth = 40;
    public int attackDamage = 30;
    public bool dead = false;
    public PlayerCombat player;
    public Text TxtHealth;

    Animator Anim;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        TxtHealth.text = health.ToString() + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        Anim.SetTrigger("Attack");
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        int d = (int) Math.Round(attackDamage * UnityEngine.Random.Range(0.5f, 1.0f));
        player.TakeDamage(d);
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
