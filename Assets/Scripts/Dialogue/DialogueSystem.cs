using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private SpriteRenderer portrait;

    private Queue<string> messages;
    public bool isActive = false;

    private void Start()
    {
        DialogueTrigger.TriggerDialogue += StartDialogue;
    }

    private void Update()
    {
        if (isActive)
        {
            GetInputs();
        }
    }

    private void OnDestroy()
    {
        DialogueTrigger.TriggerDialogue -= StartDialogue;
    }

    /// <summary>
    /// Prends la saisie du clavier de l'utilisateur.
    /// </summary>
    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (messages.Count > 0)
            {
                ChangeMessage();
            }
            else
            {
                EndDiaglogue();
            }
        }
    }

    /// <summary>
    /// Commence un nouveau dialogue.
    /// </summary>
    /// <param name="messages">Les nouveaux messages du dialogue</param>
    /// <param name="characterPortrait">L'image du personnage qui parle</param>
    private void StartDialogue(string[] messages, Sprite characterPortrait)
    {
        this.messages = new Queue<string>(messages);
        Material portraitMaterial = new Material(Shader.Find("Sprites-Default"));
        portraitMaterial.mainTexture = characterPortrait.texture;

        this.portrait.material = portraitMaterial;
        isActive = true;
    }

    /// <summary>
    /// Change le message pour le prochain.
    /// </summary>
    private void ChangeMessage()
    {
        dialogueText.text = messages.Dequeue();
    }

    /// <summary>
    /// Fini le dialogue en cours.
    /// </summary>
    private void EndDiaglogue()
    {
        isActive = false;
    }
}
