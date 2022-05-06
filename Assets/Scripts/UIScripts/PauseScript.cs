using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public static bool IsPaused;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        CheckConditions();
    }

    private void CheckConditions()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || Player.IsDead || Inventory.IsOpen) return;
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
