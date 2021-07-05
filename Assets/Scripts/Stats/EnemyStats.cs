using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyStats : CharacterStat
{

    private LifeCycleManager enemyLife;
    public int targetExperience = 60;

    public Dictionary<Transform, int> attackerPlayer = new Dictionary<Transform, int>();
    Mutex mutex = new Mutex();

    public override void TakeDamage(ref int damage, Transform attacker)
    {

        mutex.WaitOne();
        base.TakeDamage(ref damage, attacker);
        mutex.ReleaseMutex();

        if (!attackerPlayer.ContainsKey(attacker))
        {
            attackerPlayer[attacker] = damage;
        } else
        {
            attackerPlayer[attacker] += damage;
        }

        Debug.Log("Attacker player: " + attacker.tag + ", Damage: " + attackerPlayer[attacker]);

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
            Debug.Log("Attacker player: " + player.Key.tag + ", Damage: " + player.Value + " " + (targetExperience * player.Value) / 100);

            player.Key.GetComponent<PlayerStat>().gainedExperience((targetExperience * player.Value) / 100);
        }

        Destroy(gameObject);

        enemyLife = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LifeCycleManager>();

        enemyLife.RespawnEnemy();

    }
}
