using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStats : CharacterStat
{

    private LifeCycleManager enemyLife;
    public int targetExperience = 100;

    public Dictionary<string, int> attackerPlayer = new Dictionary<string, int>();
    Mutex mutex = new Mutex();

    public override void TakeDamage(ref int damage, Transform attacker)
    {

        mutex.WaitOne();
        base.TakeDamage(ref damage, attacker);
        mutex.ReleaseMutex();

        if (!attackerPlayer.ContainsKey(attacker.name))
        {
            attackerPlayer[attacker.name] = damage;
        } else
        {
            attackerPlayer[attacker.name] += damage;
        }

        Debug.Log("Attacker player: " + attackerPlayer[attacker.name] + ", Damage: ");

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public override void Die()
    {
        base.Die();

        foreach (var player in attackerPlayer)
        {
            Debug.Log("Attacker player: " + player + ", Damage: " + player.Value);
        }

        Destroy(gameObject);

        enemyLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        enemyLife.RespawnEnemy();

    }
}
