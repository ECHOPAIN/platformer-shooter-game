using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    public float distance;
    public int damage;
    public float radius = 100f;
    public float force = 700f;
    public LayerMask whatIsSolid;

    public GameObject destroyEffect;

    private void Start()
    {
        Invoke("Explode", lifeTime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                hitInfo.collider.GetComponent<HealthSystem>().TakeDamage(damage);
            }
            Explode();
        }


        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }


    void Explode()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        Debug.Log(colliders.Length);
        
        foreach(Collider2D nearbyObject in colliders)
        {
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                //rb.AddExplosionForce(force, transform.position, radius);
            }

            if (nearbyObject.CompareTag("Player"))
            {
                nearbyObject.GetComponent<HealthSystem>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}