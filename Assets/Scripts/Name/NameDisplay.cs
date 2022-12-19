using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NameDisplay : MonoBehaviour
{
    [SerializeField] private NameSender nameSender;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        nameSender.SendName += RefreshDisplay;
    }

    private void OnDestroy()
    {
        nameSender.SendName += RefreshDisplay;
    }

    private void RefreshDisplay(string value)
    {
        if (value == null || value.Length == 0)
        {
            textMesh.text = "Vous êtes mort";
            return;
        }

        if (value.Length > 30)
        {
            value = value.Substring(0, 30);
        }

        textMesh.text = $"{value} est mort";
    }
}
