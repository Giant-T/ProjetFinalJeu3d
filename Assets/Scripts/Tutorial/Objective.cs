using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    public static Action OnObjectiveFinish;

    public void FinishObjective()
    {
        OnObjectiveFinish?.Invoke();
    }
}
