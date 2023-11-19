using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ruler : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedRuler = Instantiate(weaponData.Prefab);
        spawnedRuler.transform.position = transform.position;
        spawnedRuler.transform.parent = transform;
    }
}
