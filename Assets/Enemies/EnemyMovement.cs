using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public PlayerHP playerHP;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int enemyDmg;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //hp stuff
    public float enemyHP;
    public GameObject blood;
    public WaveSystem waveSystem;
    private bool dead;

    //score stuff
    public PlayerScore playerScore;
    public int scoreGiven;

    //sound
    public AudioSource skeleton;
    public AudioClip deathSound;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        dead = false;
    }

    private void Update()
    {
        //hp stuff
        if (enemyHP <= 0 && dead == false)
        {
            dead = true;
            agent.SetDestination(transform.position);
            blood.SetActive(true);
            playerScore.EnemyKilled(scoreGiven);
            skeleton.clip = deathSound;
            skeleton.Play();
            waveSystem.RemoveEnemy(this.gameObject);          
            Invoke(nameof(DestroyObject), 2.0f);
        }

        player = GameObject.Find("Player").transform;
        agent.SetDestination(player.position);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerInAttackRange && dead == false)
        {
            AttackPlayer();
        }
    }

    //hp stuff
    public void TakeDamage(int amount)
    {
        //Debug.Log(amount);
        enemyHP = enemyHP - amount;
    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }

    private void AttackPlayer()
    {
        this.agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked) 
        {
            //attack code here
            //Debug.Log("attack");
            playerHP.TakeDamage(enemyDmg);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}

