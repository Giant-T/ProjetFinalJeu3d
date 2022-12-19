using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthController : MonoBehaviour
{
    [SerializeField] private float health = 10;
    public UnityEvent OnDeath;

    private Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;

        PlayerHittable.HitEvent += TakeDamage;
    }

    private void OnDestroy()
    {
        PlayerHittable.HitEvent -= TakeDamage;
    }

    private void TakeDamage(float damage)
    {
        SetHealth(health - damage);
    }

    private void SetHealth(float value)
    {
        health = value;
        healthSlider.value = value;

        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
