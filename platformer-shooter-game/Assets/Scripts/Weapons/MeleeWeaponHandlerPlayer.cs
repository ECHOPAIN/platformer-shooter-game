using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponHandlerPlayer : MonoBehaviour
{
    public MeleeWeapon weapon;

    // Update is called once per frame
    void Update()
    {
        //TODO only rotate shotPoint not graphic

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + weapon.offset);

        if (Input.GetMouseButton(0))
            weapon.shoot();
    }
}
