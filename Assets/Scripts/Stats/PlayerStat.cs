using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{

    private LifeCycleManager playerLife;
    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        playerLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        playerLife.RespawnPlayer();

    }
}
