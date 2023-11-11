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
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
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
        Vector3 position = transform.position;

        if (dirx < 0 && diry == 0) //left
        {
            rotation.z = 90f;
            position.x = transform.position.x -0.5f;
        }
        else if (dirx == 0 &&  diry < 0) //down
        {
            rotation.z = 180f;
            position.y = transform.position.y - 0.5f;
        }
        else if (dirx == 0 && diry > 0) //up
        {
            rotation.z = 0f;
            position.y = transform.position.y + 0.5f;

        }
        else if (dir.x > 0 && dir.y > 0) //right up
        {
            rotation.z = -45f;
            position.x = transform.position.x + 0.5f;
            position.y = transform.position.y + 0.5f;
        }
        else if (dir.x > 0 && dir.y < 0) //right down
        {
            rotation.z = -145f;
            position.x = transform.position.x + 0.5f;
            position.y = transform.position.y - 0.5f;
        }
        else if (dir.x < 0 && dir.y > 0) //left up
        {
            rotation.z = 45f;
            position.x = transform.position.x - 0.5f;
            position.y = transform.position.y + 0.5f;
        }
        else if (dir.x < 0 && dir.y < 0) //left down
        {
            rotation.z = 145f;
            position.x = transform.position.x - 0.5f;
            position.y = transform.position.y - 0.5f;
        }
        
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = position;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(), transform.position);
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
