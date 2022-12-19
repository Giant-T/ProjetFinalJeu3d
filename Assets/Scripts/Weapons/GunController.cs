using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class GunController : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    private int[] bulletsPerGun;
    private int weaponIndex = 0;
    [SerializeField] private GameObject impactPrefab;
    [SerializeField] private Barrel barrel;

    private Camera cam;

    private bool canShoot = true;
    private bool canReload = true;

    private AudioSource audioSource;
    private SpriteRenderer sprite;

    public event Action<int> UpdateBulletCount;

    private void Start()
    {
        bulletsPerGun = weapons.Select(w => w.maxBulletNumber).ToArray();

        cam = Camera.main;

        audioSource = GetComponent<AudioSource>();

        sprite = GetComponent<SpriteRenderer>();

        SetWeaponInfo();
    }

    private void Update()
    {
        if (DialogueSystem.Instance.isActive)
            return;

        GetInputs();
    }

    /// <summary>
    /// Change les informations de l'arme.
    /// </summary>
    private void SetWeaponInfo()
    {
        audioSource.clip = weapon.shotSound;

        sprite.sprite = weapon.sprite;

        barrel.sprites = weapon.chamberSprites;
        UpdateBulletCount?.Invoke(numberOfBullets);
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponIndex++;
            if (weaponIndex >= weapons.Length)
            {
                weaponIndex = 0;
            }

            SetWeaponInfo();
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
        UpdateBulletCount?.Invoke(numberOfBullets);
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

        RaycastHit hit;

        for (int i = 0; i < weapon.pelletsPerShot; i++)
        {
            if (Physics.Raycast(cam.transform.position, Quaternion.Euler(UnityEngine.Random.Range(-weapon.bulletSpread, weapon.bulletSpread), UnityEngine.Random.Range(-weapon.bulletSpread, weapon.bulletSpread), 0) * cam.transform.forward, out hit, weapon.range))
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

    private Weapon weapon
    {
        get
        {
            return weapons[weaponIndex];
        }
    }

    private int numberOfBullets
    {
        get
        {
            return bulletsPerGun[weaponIndex];
        }
        set
        {
            bulletsPerGun[weaponIndex] = value;
        }
    }
}
