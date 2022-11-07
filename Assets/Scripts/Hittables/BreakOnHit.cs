using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class BreakOnHit : Hittable
{
    private SpriteRenderer spriteRenderer;

    private void Start() {
        CreateParticle();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Hit()
    {
        spriteRenderer.enabled = false;
        particleSystem.Play();
        Destroy(gameObject, 0.5f);
    }
}
