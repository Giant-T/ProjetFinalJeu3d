using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    /// <summary>
    /// Permet au joueur de quitter le jeu.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
