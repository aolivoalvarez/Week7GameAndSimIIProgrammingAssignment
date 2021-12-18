using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform player2;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    public int index;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();

  
    }


    void Update ()
    {
        if (MultiplayerManager.pressed)
        {
            player2 = GameObject.FindGameObjectWithTag("Player 1").transform;

            float d1 = Vector3.Distance(player.position, transform.position);
            float d2 = Vector3.Distance(player2.position, transform.position);

            if (d1 < d2)
            {
                if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0)
            {
                    nav.SetDestination(player.position);
                    index = 1;
                }
                else
                {
                    nav.enabled = false;
                }
            }
            else
            {
                if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth2 > 0)
            {
                    nav.SetDestination(player2.position);
                    index = 2;
                }
                else
                {
                    nav.enabled = false;
                }
            }
        }
        else
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false;
            }
        }

       
    }
}
