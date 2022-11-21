using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private string[] messages;
    [SerializeField] private Sprite characterPortrait;

    public static Action<string[], Sprite> TriggerDialogue;

    public void SendTrigger()
    {
        TriggerDialogue?.Invoke(messages, characterPortrait);
    }
}
