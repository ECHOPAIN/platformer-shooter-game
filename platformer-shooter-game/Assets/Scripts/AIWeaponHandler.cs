using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class AIWeaponHandler : MonoBehaviour
{

    public float offset;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;
    public LayerMask whatIsSolid;

    public EnemyAi enemyAi;
    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = enemyAi.targetPlayer.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (timeBtwShots <= 0)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(shotPoint.position, shotPoint.right, 20, whatIsSolid);
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    //Destroy(Instantiate(shotEffect, shotPoint.position, shotPoint.rotation),0.1f);
                    //camAnim.SetTrigger("shake");
                    //CameraShaker.Instance.ShakeOnce(1f, 0.1f, 0f, .1f);
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


    }
}