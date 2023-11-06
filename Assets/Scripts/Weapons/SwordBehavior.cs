using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehavior : ProjectileWeapon
{
    Sword sword;
    public ParticleSystem swordParticle;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        sword = FindAnyObjectByType<Sword>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(swordParticle, transform.position, Quaternion.identity);
    }
}