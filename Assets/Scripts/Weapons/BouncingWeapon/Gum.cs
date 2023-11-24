using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gum : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        if (weaponData.ProjectileNumber == 1)
        {
            GameObject spawnedGum = Instantiate(weaponData.Prefab);
            spawnedGum.transform.position = transform.position;
            spawnedGum.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
        }
        else
        {
            int projectileNumber = weaponData.ProjectileNumber;
            
            for (int i = 0; i < projectileNumber; i++)
            {
                float offset = i * 0.2f;
                
                GameObject spawnedGum = Instantiate(weaponData.Prefab);
                spawnedGum.transform.position = transform.position + new Vector3(offset, offset, 0);
                spawnedGum.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
            }
        }
    }
}
