using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;

    public bool isGameStart = false;


    [Header("InGameEffectObj")]
    public GameObject playerHitImage = null;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    private void Awake()
    {
        Time.timeScale = 0;

        gameStartPanel.SetActive(true);
        gameOverPanel.SetActive(false);

        playerHitImage.SetActive(false);
    }

    private void Start()
    {
        EventManager.StartListening(ConstantManager.PLAYER_DAMAGED_EF, PlayerDamaged);

        ConnectScoreText();
    }

    private void Update()
    {
        ConnectScoreText();
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.gameState == DefineManager.GameState.Menu)
        {
            GameStart();
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening(ConstantManager.PLAYER_DAMAGED_EF, PlayerDamaged);
    }

    public void ConnectScoreText()
    {
        scoreText.text = $"Score : {GameManager.Instance.score}";
    }

    public void GameStart()
    {
        GameManager.Instance.gameState = DefineManager.GameState.Playing;
        isGameStart = true;

        Time.timeScale = 1;

        Sequence seq = DOTween.Sequence();

        seq.Append(gameStartPanel.transform.DOScale(1.1f, 0.3f));
        seq.Append(gameStartPanel.transform.DOScale(0f, 0.1f));

        seq.Play().OnComplete(() =>
        {
            gameStartPanel.SetActive(false);
        });
    }

    public void GameOverPanel()
    {
        GameManager.Instance.gameState = DefineManager.GameState.GameOver;
        gameOverPanel.SetActive(true);
    }

    public void ReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void PlayerDamaged()
    {
        StartCoroutine(PlayerDamagedEffectCor());
    }

    private IEnumerator PlayerDamagedEffectCor()
    {
        playerHitImage.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerHitImage.SetActive(false);

        yield break;
    }
}
