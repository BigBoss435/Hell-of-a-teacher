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
        if (weaponData.ProjectileNumber == 1)
        {
            GameObject spawnedSword = Instantiate(weaponData.Prefab);
            spawnedSword.transform.position = transform.position;
            spawnedSword.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
        }
        else
        {
            int projectileNumber = weaponData.ProjectileNumber;

            for (int i = 0; i < projectileNumber; i++)
            {
                float offset = i * 0.2f; 
                
                GameObject spawnedSword = Instantiate(weaponData.Prefab);
                spawnedSword.transform.position = transform.position + new Vector3(offset, offset, 0);
                spawnedSword.GetComponent<SwordBehavior>().DirectionChecker(pm.lastMovedVector);
                
            }
        }
    }
}