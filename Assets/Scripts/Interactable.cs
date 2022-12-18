using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Interactable : MonoBehaviour
{
    public UnityEvent OnInteract;

    /// <summary>
    /// Permet d'intéragir avec un objet.
    /// </summary>
    public void Interact()
    {
        OnInteract?.Invoke();
    }
}
