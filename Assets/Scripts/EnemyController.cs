using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 basePosition;
    private Transform player;
    public float lookRadius = 4f;
    private CharacterStat characterStat;
    public Dictionary<string, Transform> attackerPlayer = new Dictionary<string, Transform>();
    Coroutine co;

    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterStat = GetComponent<CharacterStat>();
        basePosition = transform.position;
        co = StartCoroutine(WalkAround());

    }

    // Update is called once per frame
    void Update()
    {

        ArrayList players = TriggerObjectWithInBound(lookRadius, "Player");

        if (players.Count != 0)
        {
            Collider temp = (Collider)players[0];
            player = temp.GetComponent<Transform>();
        }

        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < lookRadius)
            {
                agent.destination = player.transform.position;
                flag = true;

                StopCoroutine(co);

                // if player do not attack, enemy intent to attack player by default
                if (attackerPlayer.Count == 0 && Vector3.Distance(player.transform.position, transform.position) < 2f)
                {

                    characterStat.Attack(player.GetComponent<CharacterStat>());
                }
            }
            else
            {
                if (flag)
                {
                    flag = false;
                    co = StartCoroutine(WalkAround());
                }
            }
        }

        Transform maxDealtDamagePlayer = FindMaxDamageDealtPlayer(attackerPlayer);



        //foreach (Transform item in attackerPlayer.Values)
        //{
        //    Debug.Log("Player damages: " + item.GetComponent<CharacterStat>().damage.GetValue());
        //}

        //Debug.Log("   ------------   ");

        //if (Vector3.Distance(player.transform.position, transform.position) < 2f)
        //{

        //    characterStat.Attack(player.GetComponent<CharacterStat>());
        //}


        //ArrayList players = TriggerObjectWithInBound(2f, "Player");

        //Collider maxDealtDamagePlayer = FindMaxDamageDealtPlayer(players);

        if (maxDealtDamagePlayer != null)
        {
            Debug.Log("maxDealtDamagePlayer: " + maxDealtDamagePlayer.tag + " " + maxDealtDamagePlayer.GetComponent<CharacterStat>().damage.GetValue());
            characterStat.Attack(maxDealtDamagePlayer.GetComponent<CharacterStat>());
        }
        else
        {
            //Debug.Log("There is no enemy around!");
        }
    }


    ArrayList TriggerObjectWithInBound(float radious, string tag)
    {
        Collider[] thingsInBounds = Physics.OverlapSphere(transform.position, radious);

        ArrayList TargetObject = new ArrayList();

        foreach (Collider thing in thingsInBounds)
        {
            if (thing.tag == tag)
            {
                TargetObject.Add(thing);
            }
        }

        return TargetObject;
    }

    Transform FindMaxDamageDealtPlayer(Dictionary<string, Transform> players)
    {
        Transform maxDealtDamagePlayer = null;
        int maxDamage = 0;

        foreach (Transform player in players.Values)
        {
            if (player.GetComponent<CharacterStat>().damage.GetValue() > maxDamage)
            {
                maxDamage = player.GetComponent<CharacterStat>().damage.GetValue();
                maxDealtDamagePlayer = player;
            }
        }

        return maxDealtDamagePlayer;
    }

    //Collider FindMaxDamageDealtPlayer(ArrayList players)
    //{
    //    Collider maxDealtDamagePlayer = null;
    //    int maxDamage = 0;

    //    foreach (Collider player in players) {
    //        if (player.GetComponent<CharacterStat>().damage.GetValue() > maxDamage)
    //        {
    //            maxDamage = player.GetComponent<CharacterStat>().damage.GetValue();
    //            maxDealtDamagePlayer = player;
    //        }
    //    }

    //    return maxDealtDamagePlayer;
    //}

    IEnumerator WalkAround()
    {
        while(true)
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
