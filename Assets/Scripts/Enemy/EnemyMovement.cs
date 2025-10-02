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

    void Update()
    {
        if (EnemyHealth != null && playerHealth != null && playerHealth.currentHealth > 0 && agent != null && agent.enabled && agent.isActiveAndEnabled)
            {
                agent.SetDestination(player.position);
            }
        else if (agent != null && agent.enabled)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
}
