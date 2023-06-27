using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainScreen : MonoBehaviour
{
    private SceneLoader _sceneLoader;

    private void Awake()
    {
        Time.timeScale = 1f;
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    public void OnStartButtonClicked()
    {
        // Load the next scene...
        _sceneLoader.LoadScene(1);
    }
    public void OnLeaveButtonClicked()
    {
        Application.Quit();
    }
}
