using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera cam;
    
    private Animator animator;

    private void Start() {
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
    }

    private void Shoot()
    {
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
}
