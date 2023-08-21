using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;

    public bool isGameStart = false;

    void Awake()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    public void GameStart()
    {
        isGameStart = true;

        Time.timeScale = 1;

        Sequence seq = DOTween.Sequence();

        seq.Append(gameStartPanel.transform.DOScale(1.1f, 0.1f));
        seq.Append(gameStartPanel.transform.DOScale(0f, 0.2f));

        seq.Play().OnComplete(() =>
        {
            gameStartPanel.SetActive(false);
        });
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
