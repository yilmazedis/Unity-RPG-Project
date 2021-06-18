using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStat
{

    private LifeCycleManager enemyLife;

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);

        enemyLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        enemyLife.RespawnEnemy();

    }
}
