using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{

    private LifeCycleManager playerLife;

    public Stat level;
    public int experience = 0;

    public override void TakeDamage(ref int damage, Transform attacker)
    {

        base.TakeDamage(ref damage, attacker);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        playerLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        playerLife.RespawnPlayer();

    }
}
