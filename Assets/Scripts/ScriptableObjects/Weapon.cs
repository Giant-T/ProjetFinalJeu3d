using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public AudioClip shotSound;
    public int pelletsPerShot;
    public int maxBulletNumber;

    public float bulletSpread;
    public float range;

    [Header("Delays")]
    public float secondsBetweenShots;
    public float secondsBetweenReloads;

    [Header("Display")]
    public Sprite sprite;
    public Sprite shootSprite;
    public Sprite[] chamberSprites;
    public string animatorControllerPath;
}
