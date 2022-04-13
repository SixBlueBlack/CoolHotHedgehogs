using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool IsPaused;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        if (IsPaused)
            Resume();
        else
            Pause();
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        IsPaused = !IsPaused;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        IsPaused = !IsPaused;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        IsPaused = !IsPaused;
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Debug.Log("Just believe that you're out");
        Application.Quit();
    }
}
