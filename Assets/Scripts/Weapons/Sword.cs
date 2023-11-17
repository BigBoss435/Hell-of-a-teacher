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
        spawnedSword.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}