using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    //My variables
    [SerializeField] SO_Score scoreNum;

    private NavMeshAgent agent;
    private Rigidbody rb;
    private float sinkTime;

    private static readonly int hashDead = Animator.StringToHash("Dead");

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
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
        scoreNum.score += scoreValue;
        StartCoroutine(sink());
        Destroy (gameObject, 2f);
    }

    IEnumerator sink()
    {
        /*sinkTime += Time.deltaTime;
        if(sinkTime < 2)
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);*/ //Not working
        yield return new WaitForSeconds(2);
        Debug.Log("Activates");
    }
}