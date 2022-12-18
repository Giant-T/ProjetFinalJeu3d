using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Barrel : MonoBehaviour
{
    [SerializeField] private Gun gun;
    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        RefreshSprite(sprites.Length - 1);
        gun.UpdateBulletCount += RefreshSprite;
    }

    private void OnDestroy()
    {
        gun.UpdateBulletCount -= RefreshSprite;
    }

    /// <summary>
    /// Rafraichit le nombre de balle affichées.
    /// </summary>
    /// <param name="numberOfBullets">Le nombre de balles à afficher.</param>
    private void RefreshSprite(int numberOfBullets)
    {
        if (numberOfBullets >= sprites.Length)
            return;

        audioSource.Play();
        spriteRenderer.sprite = sprites[numberOfBullets];
    }
}
