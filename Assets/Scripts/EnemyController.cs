using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 basePosition;
    public Transform player;
    public float lookRadius = 4f;
    private CharacterStat characterStat;

    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterStat = GetComponent<CharacterStat>();
        basePosition = transform.position;
        StartCoroutine(WalkAround());

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < lookRadius)
        {
            agent.destination = player.transform.position;
            flag = true;
        } else
        {
            if (flag)
            {
                flag = false;
                StartCoroutine(WalkAround());
            }
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        {

            characterStat.Attack(player.GetComponent<CharacterStat>());
        }

        TriggerObjectWithInBound();
    }

    void TriggerObjectWithInBound()
    {
        Collider[] thingsInBounds = Physics.OverlapSphere(transform.position, 6f);
        foreach (Collider thing in thingsInBounds)
        {
            if (thing.tag == "Player")
            {
                Debug.Log(thing.name);
            }

            
        }
    }

    IEnumerator WalkAround()
    {
        while(Vector3.Distance(player.transform.position, transform.position) >= lookRadius)
        {
            agent.destination = new Vector3(basePosition.x + Random.Range(0f, 3f), 0, basePosition.z + Random.Range(0f, 3f));
            yield return new WaitForSeconds(2f);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
