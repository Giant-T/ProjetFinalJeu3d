using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Breakable", menuName = "Hittables/Breakable")]
public class Breakable : ScriptableObject
{
    public Sprite sprite;
    public Sprite spriteParticles;
}
