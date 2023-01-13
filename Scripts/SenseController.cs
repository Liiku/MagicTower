using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class SenseController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void ContinueClick()
    {
        Time.timeScale = 1;
    }

    public void RestartClick()
    {
        SceneManager.LoadScene("Game");
    }

}
