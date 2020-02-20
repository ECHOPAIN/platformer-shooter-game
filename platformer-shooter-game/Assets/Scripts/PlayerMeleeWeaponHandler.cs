using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeWeaponHandler : MonoBehaviour
{

    public float offset;

    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Handles the weapon rotation
        
        //TODO only rotate shotPoint not graphic

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(shotPoint.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if(enemiesToDamage[i]!=gameObject)
                        enemiesToDamage[i].GetComponent<HealthSystem>().TakeDamage(damage);
                }
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shotPoint.position, attackRange);
    }
}
