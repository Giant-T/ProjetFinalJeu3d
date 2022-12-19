using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SceneChanger))]
public class TutorialObjectiveController : MonoBehaviour
{
    [SerializeField] private int totalNumberOfObjectives = 3;
    private int numberOfFinishedObjectives = 0;
    private SceneChanger sceneChanger;

    public UnityEvent OnObjectiveComplete;

    private void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        Objective.OnObjectiveFinish += FinishObjective;
    }

    private void OnDestroy()
    {
        Objective.OnObjectiveFinish -= FinishObjective;
    }

    private void FinishObjective()
    {
        numberOfFinishedObjectives++;

        if (numberOfFinishedObjectives >= totalNumberOfObjectives)
        {
            OnObjectiveComplete?.Invoke();
            StartCoroutine(WaitForDialogue());
        }
    }

    private IEnumerator WaitForDialogue()
    {
        while (DialogueSystem.Instance.isActive)
        {
            yield return null;
        }

        sceneChanger.ChangeScene();
    }
}
