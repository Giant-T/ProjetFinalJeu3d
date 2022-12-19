using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float damage = 1;
    [SerializeField] float secondsOfExistence;

    private Vector3 direction = Vector3.zero;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        Destroy(gameObject, secondsOfExistence);
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// Change la direction de la balle.
    /// </summary>
    /// <param name="direction">La nouvelle direction de la balle.</param>
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }

    /// <summary>
    /// Déplace la balle dans la direction selon sa vitesse.
    /// </summary>
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision other)
    {
        Hittable hittable = other.gameObject.GetComponent<Hittable>();

        if (hittable != null)
        {
            hittable.Hit(damage);
        }

        Destroy(gameObject);
    }
}
