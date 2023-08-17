using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1;
            gameStartPanel.SetActive(false);
        }
    }


    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
