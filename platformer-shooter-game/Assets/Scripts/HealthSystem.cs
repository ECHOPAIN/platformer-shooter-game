using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class HealthSystem : MonoBehaviour
{

    //public Animator camAnim;
    public int maxHealth;
    public GameObject deathEffect;
    //public GameObject explosion;
    public SpriteRenderer[] bodyParts;
    public Color hurtColor;
    public HealthBar healthBar;
    public Transform spawnPoint;

    private int currentHealth;
    private bool isDead;
    public Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    void Start()
    {
        Setup();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            //camAnim.SetTrigger("shake");
            //Instantiate(explosion, transform.position, Quaternion.identity);
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(Flash());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator Flash()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = Color.white;
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        SetDefaults();
        transform.position = spawnPoint.position;
    }


    private void Die()
    {
        isDead = true;

        //die annimation
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        CameraShaker.Instance.ShakeOnce(15f, 0.1f, 0.5f, 1.5f);
        //Debug.Log("died");

        //disable player component
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        CircleCollider2D[] _col = GetComponents<CircleCollider2D>();

        for (int i = 0; i < _col.Length; i++)
        {
            if (_col != null)
            {
                _col[i].enabled = false;
            }
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        //respawn
        StartCoroutine(Respawn());
    }

    private void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }


        SetDefaults();
    }

    private void SetDefaults()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        CircleCollider2D[] _col = GetComponents<CircleCollider2D>();
        for (int i = 0; i < _col.Length; i++)
        {
            if(_col != null)
                _col[i].enabled = true;
        }
    }
}