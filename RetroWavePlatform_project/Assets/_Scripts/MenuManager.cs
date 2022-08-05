using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private UserInput _inputs;
    [SerializeField] private GameObject _pauseMenu;
    private bool _isPaused;

    private void OnEnable()
    {
        _isPaused = false;
    }

    private void Update()
    {
        if (!_inputs.IsPauseButtonPressed)
            return;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            return;
        
        Pause();
    }

    public void ClickExitGameButton()
    {
        Application.Quit();
    }

    public void Pause()
    {
        if(_isPaused)
        {
            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
            _isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
            _isPaused = true;
        }
    }
}
