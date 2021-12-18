using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;
    int following;


    Animator anim;
    GameObject player;
    GameObject player2;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    public int index;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
            index = 1;
        }

        if(other.gameObject == player2)
        {
            playerInRange = true;
            index = 2;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }

        if (other.gameObject == player2)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        if (MultiplayerManager.pressed)
        {
            player2 = GameObject.FindGameObjectWithTag("Player 1");
        }

            timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth1 <= 0 || playerHealth.currentHealth2 <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth1 > 0 || playerHealth.currentHealth2 > 0)
        {
            playerHealth.TakeDamage (attackDamage, index);
        }
    }
}
