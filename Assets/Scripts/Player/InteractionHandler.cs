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
        if (DialogueSystem.Instance.isActive)
        {
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

    /// <summary>
    /// Dessine la zone d'intéraction.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, zoneRadius);
    }

    /// <summary>
    /// Vérifie si un object qui a un interaction est dans la zone.
    /// </summary>
    /// <param name="interactable">La référence de sortie de l'Interactable.</param>
    /// <returns>Vrai si un Interactable est dans la zone. | Faux s'il n'y a pas d'Interactable dans la zone.</returns>
    private bool InteractableIsInRange(ref Interactable interactable)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, zoneRadius);

        if (colliders.Length < 1)
            return false;

        interactable = colliders[0].GetComponent<Interactable>();

        return interactable;
    }

    /// <summary>
    /// Change l'affichage du 
    /// </summary>
    /// <param name="isInZone">S'il y a un Interactable dans la zone.</param>
    private void TriggerChange(bool isInZone)
    {
        interactableIsInZone = isInZone;
        keyPrompt.SetActive(isInZone);
    }
}
