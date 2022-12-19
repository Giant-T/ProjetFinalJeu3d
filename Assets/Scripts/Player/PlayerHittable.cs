using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHittable : MonoBehaviour, Hittable
{
    public static Action<float> HitEvent;

    public void Hit(float damage)
    {
        HitEvent?.Invoke(damage);
    }
}
