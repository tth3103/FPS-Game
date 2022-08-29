using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Animator anim;
    public int health;
    public float pauseTimer = 1f;

    public AudioSource gunShot;
    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Chasing 
    public float maxDistance = 1;
    public float maxTime = 1f;

    //Shooting
    public float timeBetweenAttack;
    private bool shooting=false;
    bool alreadyAttacked;

    //state
    public float sightRange, shootRange;
    public bool playerInSightRange, playerInShootRange;
    float lastDidSomething=1f;
    float pauseTime = 1f;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        walkPointRange = 3;
        health = 30;
        sightRange = 7;
        shootRange = 9;
        maxTime = 0.5f;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Time.time < lastDidSomething + pauseTime) return;
        pauseTimer -= Time.deltaTime;

        if (!playerInShootRange) 
        {
            shooting = false;
        }
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInShootRange = Physics.CheckSphere(transform.position, shootRange, whatIsPlayer);

        if (!playerInSightRange && !playerInShootRange) 
        { 
            Patrolling(); 
        }
        if (playerInSightRange && !playerInShootRange) 
        {
            Chasing(); 
        }
        if (playerInSightRange && playerInShootRange) 
        { 
            Shooting(); 
        }

        //Animation
        anim.SetFloat("speed", agent.velocity.magnitude);
        anim.SetBool("shooting", shooting);
    }
    private void Patrolling()
    {
        Debug.Log("Walking");
        transform.LookAt(walkPoint);
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
        lastDidSomething = Time.time;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint,-transform.up,2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void Chasing()
    {
        Debug.Log("Chasing");
        if (pauseTimer < 0.0f)
        {
            float sqrDistance = (player.position - agent.destination).sqrMagnitude;
            if(sqrDistance < maxDistance * maxDistance)
            {
                agent.SetDestination(player.position);
            }
            pauseTimer = maxTime;
        }
        transform.LookAt(player);
        lastDidSomething = Time.time;

    }
    private void Shooting()
    {
        Debug.Log("Shooting");
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            shooting = true;
            alreadyAttacked = true;
            gunShot.Play();
            Invoke(nameof(ShootingCD), timeBetweenAttack);
        }
        lastDidSomething = Time.time;
    }
    private void ShootingCD()
    {
        alreadyAttacked = false;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Invoke(nameof(DestroyEnemy),0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
