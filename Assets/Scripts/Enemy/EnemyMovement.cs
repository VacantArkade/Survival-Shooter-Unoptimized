using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //My variables
    private EnemyHealth EnemyHealth;
    private NavMeshAgent agent;
    private PlayerHealth playerHealth;
    private Transform playerPos;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = GetComponent<EnemyHealth>();
        playerHealth = GetComponent<PlayerHealth>();
        playerPos = GetComponent<Transform>().transform;
    }

    void Update ()
    {
        Transform player = FindObjectOfType<PlayerMovement>().transform;

        if (EnemyHealth.currentHealth > 0 && player.GetComponent<PlayerHealth>().currentHealth > 0)
        {
            agent.SetDestination (player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
