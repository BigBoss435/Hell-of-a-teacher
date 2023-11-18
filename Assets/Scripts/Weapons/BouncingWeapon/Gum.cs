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
        GameObject spawnedGum = Instantiate(weaponData.Prefab);
        spawnedGum.transform.position = transform.position;
        spawnedGum.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}
