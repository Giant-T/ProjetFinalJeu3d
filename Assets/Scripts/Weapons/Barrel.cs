using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Barrel : MonoBehaviour
{
    private const int MAX_NUMBER_BULLETS = 6;

    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Gun gun;
    
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RefreshSprite(MAX_NUMBER_BULLETS);
        gun.UpdateBulletCount += RefreshSprite;
    }

    private void OnDestroy() {
        gun.UpdateBulletCount -= RefreshSprite;
    }

    private void RefreshSprite(int numberOfBullets)
    {
        spriteRenderer.sprite = sprites[numberOfBullets];
    }

}
