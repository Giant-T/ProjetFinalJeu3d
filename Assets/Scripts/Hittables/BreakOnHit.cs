using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class BreakOnHit : MonoBehaviour, Hittable
{
    [SerializeField] Breakable breakable;

    [Header("Particles")]
    [SerializeField] private Shader particleShader;
    [SerializeField] private new ParticleSystem particleSystem;
    [SerializeField] private ParticleSystemRenderer particleSystemRenderer;

    private SpriteRenderer spriteRenderer;

    protected Sprite particleSprite;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GenerateCollider();
        SetValues();
        CreateParticle();
    }

    /// <summary>
    /// Crée les particules pour le tir.
    /// </summary>
    protected void CreateParticle()
    {
        Material material = new Material(particleShader);
        material.mainTexture = particleSprite.texture;
        material.name = "Shattered Pieces";

        particleSystemRenderer.material = material;
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

    public void Hit(float _damage)
    {
        spriteRenderer.enabled = false;
        particleSystem.Play();
        Destroy(gameObject, 0.5f);
    }
}
