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


    [Header("InGameEffectObj")]
    public GameObject playerHitImage = null;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStart();
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening(ConstantManager.PLAYER_DAMAGED_EF, PlayerDamaged);
    }

    public void GameStart()
    {
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
        yield return new WaitForSeconds(0.2f);
        playerHitImage.SetActive(false);

        yield break;
    }
}
