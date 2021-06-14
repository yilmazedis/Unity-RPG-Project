using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    private Transform enemy;
    private CharacterStat characterStat;
    NavMeshAgent agent;
    Coroutine co;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        characterStat = GetComponent<CharacterStat>();
        agent = GetComponent<NavMeshAgent>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                motor.MoveToPoint(hit.point);
            }

            // if attack started before, stop attack
            if (co != null)
            {
                StopCoroutine(co);
                if (enemy != null && enemy.GetComponent<EnemyController>().attackerPlayer.Count != 0)
                {   
                    // TODO: check if instance id doesnt exist.
                    enemy.GetComponent<EnemyController>().attackerPlayer.Remove(gameObject.GetInstanceID().ToString());
                }
                enemy = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                Transform selected = hit.transform;

                Debug.Log("We hit tag " + hit.transform.tag + " " + hit.point);

                // if selected target is enemy start attack
                if (selected.transform.tag == "Enemy")
                {
                    enemy = selected;
                    co = StartCoroutine(Chase(selected.position));
                }
            }
        }

        // Attack, Enemy and co (StartCoroutine) should not be null
        if (co != null && enemy != null && Vector3.Distance(enemy.transform.position, transform.position) < 2f)
        {
            Debug.Log(gameObject.GetInstanceID().ToString());
            enemy.GetComponent<EnemyController>().attackerPlayer[gameObject.GetInstanceID().ToString()] = transform;
            characterStat.Attack(enemy.GetComponent<CharacterStat>());
        }

    }

    IEnumerator Chase(Vector3 position)
    {
        while (true)
        {
            agent.destination = position;
            yield return new WaitForSeconds(0.1f);
        }

    }


}