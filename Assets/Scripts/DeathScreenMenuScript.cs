using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenMenuScript : MonoBehaviour
{
    public GameObject DeathScreenMenu;

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P)) return;
        Player.IsDead = true;
        if (!Player.IsDead) return;
        DeathScreenMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Retry()
    {
        DeathScreenMenu.SetActive(false);
        Time.timeScale = 1;
        Player.IsDead = false;
        SceneManager.LoadScene("Game");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        Player.IsDead = false;
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Debug.Log("Just believe that you're out");
        Application.Quit();
    }
}
