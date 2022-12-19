using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class NameSender : MonoBehaviour
{
    public Action<string> SendName;
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(TriggerNameSend);
    }

    private void TriggerNameSend(string value)
    {
        SendName(value);
    }
}
