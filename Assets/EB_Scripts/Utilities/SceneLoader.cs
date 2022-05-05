using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// This is a trusty little Scene handling script.
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
