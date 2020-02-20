using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponHandlerAI : MonoBehaviour
{
    public RangedWeapon weapon;
    public EnemyAi enemyAi;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 difference = enemyAi.targetPlayer.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + weapon.offset);

        RaycastHit2D hitInfo = Physics2D.Raycast(weapon.shotPoint.position, weapon.shotPoint.right, 20, weapon.whatIsSolid);
        if (hitInfo.collider != null)
            if (hitInfo.collider.CompareTag("Player"))
                weapon.shoot();
    }
}
