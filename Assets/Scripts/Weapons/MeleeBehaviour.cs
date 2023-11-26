using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;

    //Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;


    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }
    
    public float GetCurrentDamage()
    {
        float random = Random.Range(1, 3);
        float selectType = Random.Range(0, 2);
        if (selectType == 0)
        {
            currentDamage += random;
        }
        else
        {
            currentDamage -= random;
        }
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
        }
        else if (col.CompareTag("Props"))
        {
            if (col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
        }
    }
}