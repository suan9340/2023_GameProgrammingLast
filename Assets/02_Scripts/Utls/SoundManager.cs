using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region SingleTon

    private static SoundManager _instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    _instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    #endregion

    [Header("BulletAudio")]
    public AudioSource bulletAudioSource = null;
    public AudioClip bulletClip = null;

    [Header("EnemyDIe")]
    public AudioSource enemyDieSource = null;
    public AudioClip enemyDieClip = null;

    public void BulletShoot()
    {
        bulletAudioSource.PlayOneShot(bulletClip);
    }

    public void EnemyDieShoot()
    {
        enemyDieSource.PlayOneShot(enemyDieClip);
    }
}
