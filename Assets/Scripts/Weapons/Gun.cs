using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    private const int MAX_NUMBER_BULLETS = 6;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 100f;

    [Header("Delays")]
    [SerializeField] private float secondsBetweenShots = 0.5f;
    [SerializeField] private float secondsBetweenReloads = 0.25f;

    [SerializeField] private Camera cam;

    private int numberOfBullets = MAX_NUMBER_BULLETS;
    private bool canShoot = true;
    private bool canReload = true;

    private Animator animator;

    public event Action<int> UpdateBulletCount;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        if (numberOfBullets == MAX_NUMBER_BULLETS || !canReload)
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

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Hittable hittable = hit.transform.gameObject.GetComponent<Hittable>();

            if (hittable)
            {
                hittable.Hit();
            }
        }
    }

    private IEnumerator StartShotDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(secondsBetweenShots);

        canShoot = true;
    }

    private IEnumerator StartReloadDelay()
    {
        canReload = false;

        yield return new WaitForSeconds(secondsBetweenReloads);

        canReload = true;
    }
}
