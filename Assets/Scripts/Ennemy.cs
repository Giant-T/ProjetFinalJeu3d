using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Ennemy : MonoBehaviour, Hittable
{
    [SerializeField] private float health = 10f;

    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

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
