using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SingleTon

    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    public DefineManager.GameState gameState;

    [Header("Score")]
    public float score = 0f;

    private readonly WaitForSeconds scoreSec = new WaitForSeconds(3f);

    private void Start()
    {
        StartCoroutine(ScoreAddCor());
    }


    public void AddScore(float _sco)
    {
        score += _sco;
    }

    public void MinusScore(float _sco)
    {
        score -= _sco;
    }

    private IEnumerator ScoreAddCor()
    {
        while (true)
        {
            if (gameState != DefineManager.GameState.Playing)
            {
                yield return null;
            }
            else
            {
                AddScore(1f);
            }

            yield return scoreSec;
        }
    }
}

