using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable
{
    /// <summary>
    /// Déclenche action d'être atteint.
    /// </summary>
    public abstract void Hit(float damage);
}
