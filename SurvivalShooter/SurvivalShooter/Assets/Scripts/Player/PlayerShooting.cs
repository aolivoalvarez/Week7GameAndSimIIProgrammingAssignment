using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    public PlayerMovement m;


    float timer;
    Ray shootRay1 = new Ray();
    Ray shootRay2 = new Ray();
    RaycastHit shootHit1;
    RaycastHit shootHit2;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine1;
    LineRenderer gunLine2;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine1 = GetComponent <LineRenderer> ();
        gunLine2 = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1" + m.playerIndex) && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        if (m.playerIndex == 1)
        {
            gunLine1.enabled = false;
            gunLight.enabled = false;
        }

        if (m.playerIndex == 2)
        {
            gunLine2.enabled = false;
            gunLight.enabled = false;
        }
    }


    void Shoot ()
    {
        if(m.playerIndex == 1)
        {
            timer = 0f;

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine1.enabled = true;
            gunLine1.SetPosition(0, transform.position);

            shootRay1.origin = transform.position;
            shootRay1.direction = transform.forward;

            if (Physics.Raycast(shootRay1, out shootHit1, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit1.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit1.point);
                }
                gunLine1.SetPosition(1, shootHit1.point);
            }
            else
            {
                gunLine1.SetPosition(1, shootRay1.origin + shootRay1.direction * range);
            }
        }

        if(m.playerIndex == 2)
        {
            timer = 0f;

            gunAudio.Play();

            gunLight.enabled = true;

            gunParticles.Stop();
            gunParticles.Play();

            gunLine2.enabled = true;
            gunLine2.SetPosition(0, transform.position);

            shootRay2.origin = transform.position;
            shootRay2.direction = transform.forward;

            if (Physics.Raycast(shootRay2, out shootHit2, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit2.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damagePerShot, shootHit2.point);
                }
                gunLine2.SetPosition(1, shootHit2.point);
            }
            else
            {
                gunLine2.SetPosition(1, shootRay2.origin + shootRay2.direction * range);
            }
        }
    }
}