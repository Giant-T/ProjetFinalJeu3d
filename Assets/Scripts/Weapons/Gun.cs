using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject impactPrefab;
    private Camera cam;

    private int numberOfBullets;
    private bool canShoot = true;
    private bool canReload = true;

    private Animator animator;

    public event Action<int> UpdateBulletCount;

    private void Awake()
    {
        numberOfBullets = weapon.maxBulletNumber;
    }

    private void Start()
    {
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (DialogueSystem.Instance.isActive)
            return;

        GetInputs();
    }

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

    private void Reload()
    {
        if (numberOfBullets == weapon.maxBulletNumber || !canReload)
            return;

        numberOfBullets++;
        UpdateBulletCount.Invoke(numberOfBullets);
        StartCoroutine(StartReloadDelay());
    }

    private void Shoot()
    {
        if (numberOfBullets < 1 || !canShoot)
            return;

        numberOfBullets--;
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

                if (hittable)
                {
                    hittable.Hit();
                }
            }
        }
    }

    private IEnumerator StartShotDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(weapon.secondsBetweenShots);

        canShoot = true;
    }

    private IEnumerator StartReloadDelay()
    {
        canReload = false;

        yield return new WaitForSeconds(weapon.secondsBetweenReloads);

        canReload = true;
    }
}
