using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public int pelletsPerShot;
    public int maxBulletNumber;

    public float bulletSpread;
    public float range;

    [Header("Delays")]
    public float secondsBetweenShots;
    public float secondsBetweenReloads;

    [Header("Display")]
    public Sprite gunSprite;
    public AnimationClip gunShotAnimationClip;
    public Sprite[] chamberSprites;
}