using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class BossAI : MonoBehaviour
{
    [Header("Boss Attribute")]
    private int maxHealth=300;
    private int health;
    private float attackRange = 2f;
    private int damage;
    private float sightRange = 20f;
    private float attackInterval = 2f;

    [Header("Boss Component")]
    public Transform player;
    public LayerMask whatIsPlayer;
    public NavMeshAgent nav;
    public Animator anim;
    public AudioSource enrangeSound;
    public AudioSource dyingSound;
    public AudioSource attackSound;

    [Header("UI & Additional Component")]
    public GameObject bossInfo;
    public Slider hpBar;
    public GameObject barricage;
    private enum Phase
    {
        begin,
        normal,
        enraged,
        defeated
    }
    private Phase phase;

    //Combat parameter
    private bool isEnraged = false;
    private bool isVulnerable = false;
    private bool isDefeated = false;
    private bool playerInSightRange;
    private bool playerInAttackRange;
    private bool attack = false;
    private bool alreadyAttacked = false;

    void Start()
    {
        health=maxHealth;
        player = GameObject.Find("PlayerCapsule").transform;
        phase = Phase.begin;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hpBar.maxValue = maxHealth;

    }

    void Update()
    {
        //Phase Check
        checkPhase();

        //animation control
        if (!isDefeated)
        {
            anim.SetFloat("velocity", nav.velocity.magnitude);
        }
        if (!isEnraged)
        {
            anim.SetBool("Attack", attack);
            anim.SetBool("EnragedAttack", false);
        }
        else
        {
            anim.SetBool("Attack", false);
            anim.SetBool("EnragedAttack", attack);
        }
        
        //Logic check
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerInAttackRange)
            {
                attack = false;
            }
            if (!playerInAttackRange && playerInSightRange && !alreadyAttacked)
            {
            if (!isDefeated)
            {
                StartCoroutine(EnterBattle());
            }
            else
            {
                StopCoroutine(EnterBattle());
            }
                StartCoroutine(Chasing());
            }
            if (playerInAttackRange)
            {
                StartCoroutine(Attack());
            }
    }
    public void checkPhase()
    {
        switch (phase)
        {
            case Phase.begin:
                if(playerInSightRange)
                phase = Phase.normal;
                damage = 10;
                nav.speed = 3f;
                break;
            case Phase.normal:
                if(health <= 150)
                {
                    phase = Phase.enraged;
                    damage = 20;
                    nav.speed = 5f;
                    attackInterval = 3f;
                    isEnraged = true;
                    StartCoroutine(Enrage());
                    break;
                }                
                break;
            case Phase.enraged:
                if (isDefeated) phase = Phase.defeated;
                break;
        }
    }
    public IEnumerator Attack()
    {
        if (isDefeated)
        {
            yield return null;
        }
        else 
        {
            //Get to player last position
            nav.SetDestination(transform.position);
            if (!alreadyAttacked)
            {
                StopCoroutine(Chasing());
                alreadyAttacked = true;
                attack = true;
                GlobalHealth.currentHealth -= damage;
                attackSound.Play();
                Invoke(nameof(AttackCD),attackInterval);
            }
        }
    }
    private void AttackCD()
    {
        alreadyAttacked = false;
    }
    public IEnumerator Chasing()
    {
        if (isDefeated)
        {
            yield return null;
        }
        else
        {
            nav.SetDestination(player.position);
            transform.LookAt(player);
            yield return new WaitForSeconds(2f);
        }
    }
    public IEnumerator Enrage()
    {
        if (isEnraged)
        {
            isVulnerable = false;
            anim.SetTrigger("Enrage");
            enrangeSound.Play();
            yield return new WaitForSeconds(1f);
            anim.SetBool("isEnraged", isEnraged);
            yield return new WaitForSeconds(0.5f);
            isVulnerable = true;
        }
        else yield return null;
        
    }
    public IEnumerator Defeated()
    {
        anim.SetBool("isDefeated", isDefeated);
        Destroy(nav);
        barricage.SetActive(false);
        dyingSound.Play();
        EnemyCount.enemyDefeated += 1;
        yield return new WaitForSeconds(3f);
        bossInfo.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        if (!isVulnerable)
        {
            return;
        }
        else
        {
            if (isEnraged)
            {
                health -= damage;
            }
            else
            {
                health -= damage + damage * 50 / 100;
            }
            SetHealth(health);
            if (health <= 0)
            {
                isDefeated = true;
                StartCoroutine(Defeated());
            }
        }
    }
    public IEnumerator EnterBattle()
    {
        isVulnerable = true;
        bossInfo.SetActive(true);
        barricage.SetActive(true);
        yield return null;
    }
    public void SetHealth(int health)
    {
        hpBar.value = health;
    }
}
