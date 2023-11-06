using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAferSeconds;

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
        currentPierce= weaponData.Pierce;
    }
    
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }
    
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAferSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale  = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0) //left
        {
            rotation.z = 90f;
        }
        else if (dirx == 0 &&  diry < 0) //down
        {
            rotation.z = 180f;
        }
        else if (dirx == 0 && diry > 0) //up
        {
            rotation.z = 0f;
            
        }
        else if (dir.x > 0 && dir.y > 0) //right up
        {
            rotation.z = -45f;
        }
        else if (dir.x > 0 && dir.y < 0) //right down
        {
            rotation.z = -145f;
        }
        else if (dir.x < 0 && dir.y > 0) //left up
        {
            rotation.z = 45f;
        }
        else if (dir.x < 0 && dir.y < 0) //left down
        {
            rotation.z = 145f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage());
            ReducePierce();
        }
        else if (col.CompareTag("Props"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if(currentPierce <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
