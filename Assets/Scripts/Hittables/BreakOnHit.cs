using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class BreakOnHit : Hittable
{
    [SerializeField] Breakable breakable;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GenerateCollider();
        SetValues();
        CreateParticle();
    }

    /// <summary>
    /// Mets les valeurs du scriptable objects dans les components.
    /// </summary>
    private void SetValues()
    {
        particleSprite = breakable.spriteParticles;
        spriteRenderer.sprite = breakable.sprite;
    }

    /// <summary>
    /// Génère les collisions selon le sprite.
    /// </summary>
    private void GenerateCollider()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();

        float xSize = breakable.sprite.rect.width / breakable.sprite.pixelsPerUnit;
        float ySize = breakable.sprite.rect.height / breakable.sprite.pixelsPerUnit;

        Vector3 size = new Vector3(xSize, ySize, xSize);
        Vector3 center = boxCollider.center;
        center.y += size.y / 2f;

        boxCollider.size = size;
        boxCollider.center = center;
    }

    public override void Hit()
    {
        spriteRenderer.enabled = false;
        particleSystem.Play();
        Destroy(gameObject, 0.5f);
    }
}
