using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Barrel : MonoBehaviour
{
    private const int MAX_NUMBER_BULLETS = 6;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Gun gun;
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        RefreshSprite(MAX_NUMBER_BULLETS);
        gun.UpdateBulletCount += RefreshSprite;
    }

    private void OnDestroy() {
        gun.UpdateBulletCount -= RefreshSprite;
    }

    /// <summary>
    /// Rafraichit le nombre de balle affichées.
    /// </summary>
    /// <param name="numberOfBullets">Le nombre de balles à afficher.</param>
    private void RefreshSprite(int numberOfBullets)
    {
        audioSource.Play();
        spriteRenderer.sprite = sprites[numberOfBullets];
    }

}
