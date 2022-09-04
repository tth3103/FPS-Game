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
    public int maxHealth;
    public static int currentHealth;
    public float pauseTimer = 3f;

    public AudioSource gunShot;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Chasing 
    public float maxDistance = 1f;
    public float maxTime = 1f;

    //Shooting
    public float timeBetweenAttack=2f;
    private bool shooting=false;
    bool alreadyAttacked;

    //Death
    public bool isDead;

    //state
    public float sightRange, shootRange;
    public bool playerInSightRange, playerInShootRange;
    float lastDidSomething;

    private void Awake()
    {
        player = GameObject.Find("PlayerCapsule").transform;
        agent = GetComponent<NavMeshAgent>();
        walkPointRange = 3;
        maxHealth = 30;
        sightRange = 6;
        shootRange = 5;
        maxTime = 0.5f;
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        gunShot = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        isDead = false;
    }
    private void Update()
    {
        //Animation
        anim.SetFloat("speed", agent.velocity.magnitude);
        anim.SetBool("shooting", shooting);

        if (Time.time < lastDidSomething + pauseTimer) return;

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
            FacePlayer();
            Shooting(); 
        } 
    }
    private void Patrolling()
    {
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
        agent.SetDestination(player.position);
        transform.LookAt(player);
        lastDidSomething = Time.time;

    }
    private void Shooting()
    {
        agent.SetDestination(transform.position);
        if (!alreadyAttacked)
        {
            shooting = true;
            alreadyAttacked = true;
            gunShot.Play();
            GlobalHealth.currentHealth -= 10;
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
        currentHealth -= damage;
        if (currentHealth <= 0) {
            isDead = true;
            Death();
        }
    }
    private void Death()
    {
        anim.SetBool("isDead", isDead);
        EnemyCount.enemyDefeated += 1;
        GetComponent<EnemyAI>().enabled =false;
    }
    private void FacePlayer()
    {
        Vector3 direction =(player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * 200f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
