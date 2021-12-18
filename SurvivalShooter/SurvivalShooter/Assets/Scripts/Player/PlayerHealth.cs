using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth1;
    public int currentHealth2;
    public Slider healthSlider;
    public Slider p2Slider;

    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public AudioClip lowHealthClip;
    public PlayerMovement m;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    public static bool damaged;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth1 = startingHealth;
        currentHealth2 = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;


            if ((currentHealth1 <= 50 && currentHealth1 > 0) || (currentHealth2 <= 50 && currentHealth2 > 0))
            {
                lowHealth();
            }


        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount, int index)
    {
        if (index == 1)
        {
            damaged = true;

            currentHealth1 -= amount;

            healthSlider.value = currentHealth1;

            playerAudio.Play();


            if (currentHealth1 <= 0 && !isDead)
            {
                Death();
            }
        }
        else if (index == 2)
        {
            damaged = true;

            currentHealth2 -= amount;

            p2Slider.value = currentHealth2;

            playerAudio.Play();


            if (currentHealth2 <= 0 && !isDead)
            {
                Death();
            }
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    void lowHealth()
    {
            playerAudio.clip = lowHealthClip;

            playerAudio.Play();
    }
}
