using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    public float offset;

    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    public LayerMask whatIsSolid;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shotPoint.position, attackRange);
    }

    public void shoot()
    {
        if (timeBtwShots <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(shotPoint.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i] != gameObject)
                    enemiesToDamage[i].GetComponent<HealthSystem>().TakeDamage(damage);
            }
            timeBtwShots = startTimeBtwShots;
        }
    }
}
