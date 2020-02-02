using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Enemy : MonoBehaviour
{

    public Animator camAnim;
    public int health;
    public GameObject deathEffect;
    public GameObject explosion;
    public SpriteRenderer[] bodyParts;
    public Color hurtColor;

    private void Update()
    {
        if (health <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(15f, 0.1f, 0.5f, 1.5f);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        //camAnim.SetTrigger("shake");
        //Instantiate(explosion, transform.position, Quaternion.identity);
        StartCoroutine(Flash());
        health -= damage;
    }

    IEnumerator Flash()
    {
        for(int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = hurtColor;
        }
        yield return new WaitForSeconds(0.05f);
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].color = Color.green;
        }
    }
}