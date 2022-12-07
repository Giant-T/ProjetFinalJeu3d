using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;

    [Header("Text Areas")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI charName;

    [Header("Portrait")]
    [SerializeField] private Image portrait;

    private Queue<string> messages;
    public static bool isActive = false;

    private void Start()
    {
        DialogueTrigger.TriggerDialogue += StartDialogue;
    }

    private void Update()
    {
        if (isActive)
        {
            dialogueBox.SetActive(true);
            GetInputs();
        }
        else
        {
            dialogueBox.SetActive(false);
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
                EndDialogue();
            }
        }
    }

    /// <summary>
    /// Commence un nouveau dialogue.
    /// </summary>
    /// <param name="messages">Les nouveaux messages du dialogue</param>
    /// <param name="characterPortrait">L'image du personnage qui parle</param>
    private void StartDialogue(string[] messages, string charName, Sprite characterPortrait)
    {
        this.messages = new Queue<string>(messages);
        this.ChangeMessage();

        this.charName.text = charName;

        portrait.sprite = characterPortrait;

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
    private void EndDialogue()
    {
        isActive = false;
    }
}
