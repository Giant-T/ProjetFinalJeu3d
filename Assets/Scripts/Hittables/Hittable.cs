using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hittable : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] protected Shader particleShader;
    [SerializeField] protected new ParticleSystem particleSystem;
    [SerializeField] protected ParticleSystemRenderer particleSystemRenderer;

    protected Sprite particleSprite;

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
    /// Déclenche action d'être atteint.
    /// </summary>
    public abstract void Hit();
}
