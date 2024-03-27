using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    private bool isPaused = false;
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetPause(!isPaused);
        }
    }

    public void SetPause(bool shouldBePaused)
    {
        isPaused = shouldBePaused;
        _gameObject.SetActive(shouldBePaused);
        Time.timeScale = shouldBePaused ? 0 : 1;
    }

    public void QuitGameToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
