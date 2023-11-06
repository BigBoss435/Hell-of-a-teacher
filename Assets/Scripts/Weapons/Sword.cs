using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Sword : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedSword = Instantiate(weaponData.Prefab);
        spawnedSword.transform.position = transform.position;
        spawnedSword.GetComponent<SwordBehavior>().DirectionChecker(pm.moveDir);
    }
}