using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    Rigidbody2D rb;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerStats>().transform;
        rb = GetComponent<Rigidbody2D>();
        Collider2D col = rb.GetComponent<Collider2D>();

        PhysicsMaterial2D physMat = new PhysicsMaterial2D();
        physMat.friction = 0.05f;

        col.sharedMaterial = physMat;
    }

    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * enemy.currentMoveSpeed;
    }
}
