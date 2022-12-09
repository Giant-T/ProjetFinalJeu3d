using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private GameObject keyPrompt;
    [SerializeField] private float zoneRadius;

    private bool interactableIsInZone = false;

    private void Update()
    {
        Interactable interactable = null;

        if (InteractableIsInRange(ref interactable))
        {
            Debug.Log(interactable);
            if (!interactableIsInZone)
            {
                TriggerChange(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        else if (interactableIsInZone)
        {
            TriggerChange(false);
        }
    }

    private bool InteractableIsInRange(ref Interactable interactable)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, zoneRadius);

        if (colliders.Length < 1)
            return false;

        interactable = colliders[0].GetComponent<Interactable>();

        return interactable;
    }

    private void TriggerChange(bool isInZone)
    {
        interactableIsInZone = isInZone;
        keyPrompt.SetActive(isInZone);
    }
}
