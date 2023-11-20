using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScentController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSmell = Instantiate(weaponData.Prefab);
        spawnedSmell.transform.position = transform.position;
        //spawnedSmell.transform.parent = transform;
    }
}
