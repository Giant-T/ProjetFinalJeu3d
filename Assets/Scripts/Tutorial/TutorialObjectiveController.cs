using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneChanger))]
public class TutorialObjectiveController : MonoBehaviour
{
    [SerializeField] private int totalNumberOfObjectives = 3;
    private int numberOfFinishedObjectives = 0;
    private SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        Objective.OnObjectiveFinish += FinishObjective;
    }

    private void OnDestroy() {
        Objective.OnObjectiveFinish -= FinishObjective;
    }

    private void FinishObjective()
    {
        numberOfFinishedObjectives++;

        if (numberOfFinishedObjectives >= totalNumberOfObjectives)
        {
            sceneChanger.ChangeScene();
        }
    }
}
