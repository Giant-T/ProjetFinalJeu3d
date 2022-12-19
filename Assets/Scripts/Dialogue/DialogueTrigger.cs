using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private string[] messages;
    [SerializeField] private string charName;
    [SerializeField] private Sprite characterPortrait;
    [SerializeField] private bool onStart = false;

    public static Action<string[], string, Sprite> TriggerDialogue;

    private void Start()
    {
        if (onStart)
        {
            SendTrigger();
        }
    }

    // private void OnDisable()
    // {
    //     if (onStart)
    //     {
    //         SceneManager.sceneLoaded -= SendOnStartTrigger;
    //     }
    // }

    /// <summary>
    /// Lance le déclencheur de dialogue.
    /// </summary>
    public void SendTrigger()
    {
        TriggerDialogue?.Invoke(messages, charName, characterPortrait);
    }

    public void SendOnStartTrigger(Scene scene, LoadSceneMode mode)
    {
        TriggerDialogue?.Invoke(messages, charName, characterPortrait);
    }
}
