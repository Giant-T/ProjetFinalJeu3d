using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] messages;
    [SerializeField] private string charName;
    [SerializeField] private Sprite characterPortrait;

    public static Action<string[], string, Sprite> TriggerDialogue;

    public void SendTrigger()
    {
        TriggerDialogue?.Invoke(messages, charName, characterPortrait);
    }
}
