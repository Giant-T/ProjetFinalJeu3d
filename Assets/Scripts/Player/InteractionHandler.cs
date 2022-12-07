using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private float zoneRadius;
    private Collider[] colliders = new Collider[1];

    private void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, zoneRadius, colliders);

        Interactable interactable = colliders[0].GetComponent<Interactable>();

        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact();
        }
    }
}
