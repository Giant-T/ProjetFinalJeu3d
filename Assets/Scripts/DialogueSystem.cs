using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Camera))]
public class DialogueSystem : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] private string[] dialogues;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialogueCanvas;

    [Header("Player info")]
    [SerializeField] private Camera playerCam;
    [SerializeField] private GameObject player;

    private int index = 0;
    private Camera dialogueCam;

    private void Start()
    {
        dialogueCam = GetComponent<Camera>();
        RefreshText();
    }

    private void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            index++;
            if (index < dialogues.Length)
            {
                RefreshText();
            }
            else
            {
                ChangeCam();
            }
        }
    }

    private void RefreshText()
    {
        dialogueText.text = dialogues[index];
    }

    private void ChangeCam()
    {
        player.SetActive(true);
        dialogueCam.enabled = false;
        playerCam.enabled = true;

        Destroy(dialogueCanvas);
        Destroy(gameObject);
    }
}
