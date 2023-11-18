using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

    public Rigidbody2D rb;
    public Vector3 LastVelocity;
    
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
        else if (col.CompareTag("Bounds"))
        {
            ReducePierce();
            
            Vector2 normal = GetCollisionNormal(col, transform.position);
            direction = Vector2.Reflect(direction, normal);
        }
    }
    
    private Vector2 GetCollisionNormal(Collider2D collider, Vector2 collisionPoint)
    {
        // Assuming your collider is an EdgeCollider2D
        EdgeCollider2D edgeCollider = collider as EdgeCollider2D;

        if (edgeCollider != null)
        {
            // Get the closest point on the collider to the collision point
            Vector2 closestPoint = edgeCollider.ClosestPoint(collisionPoint);

            // Calculate the normal vector from the collision point to the closest point
            return (collisionPoint - closestPoint).normalized;
        }

        // Default to reflecting vertically if the collider type is not recognized
        return new Vector2(0, -1);
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
