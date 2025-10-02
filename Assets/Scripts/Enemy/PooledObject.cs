using UnityEngine;
using UnityEngine.AI;

public class PooledObject : MonoBehaviour
{
    [HideInInspector]
    public GameObject prefabReference;

    private NavMeshAgent agent;
    private Transform target;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player")?.transform;
    }

    public virtual void OnSpawned()
    {
        if (agent != null)
        {
            // Fully reset agent
            agent.enabled = false;
            agent.enabled = true;
            agent.ResetPath();
            agent.isStopped = false;

            // Ensure destination is valid
            if (target != null && agent.isOnNavMesh)
            {
                agent.SetDestination(target.position);
                Debug.Log($"[PooledObject] {gameObject.name} set destination to {target.position}");
            }
            else
            {
                Debug.LogWarning($"[PooledObject] {gameObject.name} has no valid NavMesh or target.");
            }
        }
    }

    public virtual void OnDespawned()
    {
        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }
    }
}