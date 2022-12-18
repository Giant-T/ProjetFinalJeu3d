using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private Barrel barrel;

    private Camera cam;

    private int numberOfBullets;
    private bool canShoot = true;
    private bool canReload = true;

    private Animator animator;
    private AudioSource audioSource;
    private SpriteRenderer sprite;

    public event Action<int> UpdateBulletCount;

    private void Awake()
    {
        numberOfBullets = weapon.maxBulletNumber;
    }

    private void Start()
    {
        cam = Camera.main;

        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = weapon.AnimatorController;

        audioSource = GetComponent<AudioSource>();

        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = weapon.sprite;

        barrel.sprites = weapon.chamberSprites;
    }

    private void Update()
    {
        if (DialogueSystem.Instance.isActive)
            return;

        GetInputs();
    }

    /// <summary>
    /// Prends les inputs du joueur pour savoir s'il tir ou s'il recharge.
    /// </summary>
    private void GetInputs()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown("r"))
        {
            Reload();
        }
    }

    /// <summary>
    /// Recharge l'arme.
    /// </summary>
    private void Reload()
    {
        if (numberOfBullets == weapon.maxBulletNumber || !canReload)
            return;

        numberOfBullets++;
        UpdateBulletCount.Invoke(numberOfBullets);
        StartCoroutine(StartReloadDelay());
    }

    /// <summary>
    /// Tire une balle.
    /// </summary>
    private void Shoot()
    {
        if (numberOfBullets < 1 || !canShoot)
            return;

        numberOfBullets--;
        audioSource.Play();
        UpdateBulletCount.Invoke(numberOfBullets);
        StartCoroutine(StartShotDelay());

        animator.SetTrigger("Shoot");

        RaycastHit hit;

        for (int i = 0; i < weapon.pelletsPerShot; i++)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range))
            {
                Instantiate(impactPrefab, hit.point, Quaternion.identity);

                Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();

                if (hittable != null)
                {
                    hittable.Hit(weapon.damage);
                }
            }
        }
    }

    /// <summary>
    /// Démarre le délais de tir.
    /// </summary>
    private IEnumerator StartShotDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(weapon.secondsBetweenShots);

        canShoot = true;
    }

    /// <summary>
    /// Démarre le délais de recharge.
    /// </summary>
    private IEnumerator StartReloadDelay()
    {
        canReload = false;

        yield return new WaitForSeconds(weapon.secondsBetweenReloads);

        canReload = true;
    }
}
