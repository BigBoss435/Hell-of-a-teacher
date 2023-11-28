using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathCheck : MonoBehaviour
{
    EnemyStats enemy;
    public bool hasBeenKilled = false;
    
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        CheckIfHasBeenKilled();
    }

    void CheckIfHasBeenKilled()
    {
        if (enemy.currentHealth <= 0 && !hasBeenKilled)
        {
            Debug.Log("Has been killed");
            hasBeenKilled = true;
        }
    }
}
