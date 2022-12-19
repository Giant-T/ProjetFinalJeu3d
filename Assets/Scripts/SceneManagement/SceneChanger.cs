using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneName;

    /// <summary>
    /// Change la scene.
    /// </summary>
    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
