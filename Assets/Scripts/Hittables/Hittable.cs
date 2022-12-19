using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable
{
    /// <summary>
    /// Déclenche action d'être atteint.
    /// </summary>
    void Hit(float damage);
}
