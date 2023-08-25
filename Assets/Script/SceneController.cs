using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string sceneName;

    static public void ChangeScene(string sceneName)
    {
        Debug.Log("SCENE CHANGING");
        SceneManager.LoadScene(sceneName);
    }
}
