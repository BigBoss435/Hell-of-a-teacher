using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBehavior : ProjectileWeapon
{
    Gum gum;
    public ParticleSystem gumParticle;

    protected override void Start()
    {
        base.Start();
        gum = FindObjectOfType<Gum>();
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
        Instantiate(gumParticle, transform.position, Quaternion.identity);
    }
}
