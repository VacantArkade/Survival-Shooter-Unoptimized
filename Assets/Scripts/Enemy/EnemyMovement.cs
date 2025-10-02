using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //My variables
    private EnemyHealth EnemyHealth;
    private NavMeshAgent agent;
    private PlayerHealth playerHealth;
    private Transform player;

    [SerializeField] SO_PHealth healthCon;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = GetComponent<EnemyHealth>();
        player = FindAnyObjectByType<PlayerMovement>().transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update ()
    {
        if (EnemyHealth.currentHealth > 0 && healthCon.health > 0)
        {
            agent.SetDestination (player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
}
