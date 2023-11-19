using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerBehavior : MeleeBehaviour
{
    Ruler ruler;
    public ParticleSystem rulerParticle;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ruler = FindAnyObjectByType<Ruler>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        Instantiate(rulerParticle, transform.position, Quaternion.identity);
    }
}
