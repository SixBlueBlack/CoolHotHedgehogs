using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public GameObject VictoryScreenMenu;

    void Update()
    {
        ActivateVictoryScreenMenu();
    }

    private void ActivateVictoryScreenMenu()
    {
        if (!Player.IsWin) return;
        VictoryScreenMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        Player.IsWin = false;
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Debug.Log("Just believe that you're out");
        Application.Quit();
    }
}