using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : PooledObject
{
    public int currentHealth;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    //My variables
    [SerializeField] SO_Score scoreNum;
    [SerializeField] SO_EnemyConfig stats;

    private NavMeshAgent agent;
    private Rigidbody rb;

    private static readonly int hashDead = Animator.StringToHash("Dead");

    void Awake ()
    {
        currentHealth = stats.startHealth;
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * stats.sinkSpd * Time.deltaTime);
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger(hashDead);

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        agent.enabled = false;
        rb.isKinematic = true;
        isSinking = true;
        scoreNum.score += stats.score;
        StartCoroutine(SinkAndDespawn());
    }

    private IEnumerator SinkAndDespawn()
    {
        yield return new WaitForSeconds(2f);
        PoolManager.Instance.Despawn(gameObject);
    }

    public override void OnSpawned()
    {
        isDead = false;
        isSinking = false;
        currentHealth = stats.startHealth;
        if (capsuleCollider != null)
            capsuleCollider.isTrigger = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        var nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav != null)
        {
            nav.enabled = true;
            nav.isStopped = false;
            nav.ResetPath();
        }

        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }
    }

}