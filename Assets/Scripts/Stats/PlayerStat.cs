using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{

    private LifeCycleManager playerLife;

    public Stat level; // stat
    int targetExperience { get { return level.GetValue() * 100; } }
    int experience = 0;

    public override void TakeDamage(ref int damage, Transform attacker)
    {

        base.TakeDamage(ref damage, attacker);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void gainedExperience(int exp)
    {
        experience += exp;
            
        if (experience >= targetExperience)
        {
            
            experience = experience - targetExperience;

            level.IncreaseValue(1);
            damage.IncreaseValue(2);
            armor.IncreaseValue(1);
            attackSpeed.IncreaseValue(1);
            
            Debug.Log(gameObject.tag + " level: " + level.GetValue());
        }
        Debug.Log(gameObject.tag + " exp: " + experience);
    }

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        playerLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        playerLife.RespawnPlayer();

    }
}
