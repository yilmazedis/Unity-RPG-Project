using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycleManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Vector3 playerSpawnPosition;
    public Vector3 enemySpawnPosition;

    private void Start()
    {
        playerSpawnPosition = new Vector3(12f, 1.25f, -12f);
        enemySpawnPosition = new Vector3(0, 1.25f, 0);
        
    }

    public void RespawnPlayer()
    {

        Instantiate(player, playerSpawnPosition, Quaternion.identity);
    }

    public void RespawnEnemy()
    {

        Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
    }
}
