using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Enemy : MonoBehaviour, Hittable
{
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float secondsBetweenShots;
    [SerializeField] private float range = 30;
    [SerializeField] private float health = 10f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float gravity = -10f;

    private Transform player;
    private bool playerIsInRange = false;
    private bool canShoot = true;

    private CharacterController controller;
    private AudioSource audioSource;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }

    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    private void Update()
    {
        if (health > 0)
        {
            float playerDistance = (player.position - transform.position).magnitude;
            playerIsInRange = playerDistance <= range;

            if (playerIsInRange)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }
        controller.Move(transform.up * gravity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        Vector3 lookPosition = player.position - transform.position;
        lookPosition.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPosition);
    }

    /// <summary>
    /// Permet à l'ennemi d'attaquer s'il peut sinon il attend
    /// </summary>
    private void Attack()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
            return;
        }

        animator.SetTrigger("Idle");
    }

    /// <summary>
    /// Tir une balle dans la direction que l'ennemi regarde.
    /// </summary>
    private IEnumerator Shoot()
    {
        canShoot = false;
        animator.SetTrigger("Shoot");
        GameObject bulletObject = Instantiate(bullet, gun.position, Quaternion.identity);
        bulletObject.GetComponent<Bullet>().SetDirection(transform.forward);

        yield return new WaitForSeconds(secondsBetweenShots);

        canShoot = true;
    }

    /// <summary>
    /// Déplace l'ennemi où il regarde.
    /// </summary>
    private void Move()
    {
        animator.SetTrigger("Walk");
        controller.Move(transform.forward * speed * Time.deltaTime);
    }

    public void Hit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            animator.SetTrigger("Death");
        }

        animator.SetTrigger("Hit");
        audioSource.Play();
    }
}
