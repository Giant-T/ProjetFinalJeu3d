using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; private set; }

    [SerializeField] private GameObject dialogueBox;

    [Header("Text Areas")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI charName;

    [Header("Portrait")]
    [SerializeField] private Image portrait;

    public UnityEvent OnDialogueEnd;

    private Queue<string> messages;
    private bool _isActive = false;

    public bool isActive
    {
        get
        {
            return _isActive;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
        DialogueTrigger.TriggerDialogue += Instance.StartDialogue;
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
        DialogueTrigger.TriggerDialogue -= Instance.StartDialogue;
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
        dialogueBox.SetActive(true);
        this.ChangeMessage();

        this.charName.text = charName;

        portrait.sprite = characterPortrait;

        _isActive = true;
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
        _isActive = false;
        dialogueBox.SetActive(false);
        OnDialogueEnd?.Invoke();
    }
}
