using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera myCam;
    private float shakeTimer;

    private void Awake()
    {
        Instance = this;
        myCam = GetComponent<CinemachineVirtualCamera>();
    }


    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin myCin = myCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                myCin.m_AmplitudeGain = 0f;
            }
        }
    }


    public void ShakeCamera(float _intensity, float _time)
    {
        CinemachineBasicMultiChannelPerlin myCin = myCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        myCin.m_AmplitudeGain = _intensity;
        shakeTimer = _time;
    }

}
