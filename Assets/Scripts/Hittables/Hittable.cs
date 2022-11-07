using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] protected Sprite particleSprite;
    [SerializeField] protected Shader particleShader;
    [SerializeField] protected new ParticleSystem particleSystem;
    [SerializeField] protected ParticleSystemRenderer particleSystemRenderer;

    protected void CreateParticle()
    {
        Material material = new Material(particleShader);
        material.mainTexture = particleSprite.texture;
        material.name = "Shattered Pieces";

        particleSystemRenderer.material = material;
    }

    public abstract void Hit();
}
