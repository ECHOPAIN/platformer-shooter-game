using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerWeaponHandler : MonoBehaviour
{

    public float offset;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;

    public bool infiniteAmo = false;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;

    private float timeBtwShots;
    public float startTimeBtwShots;
    private bool isRealoading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isRealoading = false;
    }

    private void Update()
    {
        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (isRealoading)
        {
            return;
        }
        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                if(!infiniteAmo)
                    currentAmmo--;
                //Destroy(Instantiate(shotEffect, shotPoint.position, shotPoint.rotation),0.1f);
                //camAnim.SetTrigger("shake");
                //CameraShaker.Instance.ShakeOnce(1f,0.1f,0f,.1f);
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


    }

    IEnumerator Reload()
    {
        isRealoading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isRealoading = false;
    }
}