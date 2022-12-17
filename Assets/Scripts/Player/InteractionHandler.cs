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
        if (DialogueSystem.Instance.isActive) {
            TriggerChange(false);
            return;
        }

        Interactable interactable = null;

        if (InteractableIsInRange(ref interactable))
        {
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

    private void OnDrawGizmos() {
       Gizmos.color = Color.yellow; 
       Gizmos.DrawWireSphere(transform.position, zoneRadius);
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
