using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
   // public CinemachineBrain camBrain;
    private CinemachineVirtualCamera cam;
    private float shakeTimer;
    private float shakerTimerTotal;
    CinemachineBasicMultiChannelPerlin noise;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time, CinemachineVirtualCamera _cam) // se for camera de interação ou cutscene, chamar esse método por lá direto
    {
        noise = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakeTimer = time;
    }

    public void ShakeCamera(float intensity, float time) // se for camera de interação ou cutscene, chamar esse método por lá direto
    {
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakeTimer = time;
    }

    public void ShakeCamera(float intensity, float frequency, float time) // se for camera de interação ou cutscene, chamar esse método por lá direto
    {
        noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = frequency;
        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakeTimer = time;
    }

    public void ShakeCamera(float intensity, float frequency, float time, CinemachineVirtualCamera _cam) // se for camera de interação ou cutscene, chamar esse método por lá direto
    {
        noise = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = frequency;
        startingIntensity = intensity;
        shakerTimerTotal = time;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                // cabou o tempo 
                noise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, (1 - shakeTimer / shakerTimerTotal)); 
                
            }
        }

        if(Input.GetKeyDown(KeyCode.K)) ShakeCamera(.4f, .1f); // para versão de teste

    }

    public void UpdateActualCam(CinemachineVirtualCamera newCam)
    {
        print(newCam.name);
        cam = newCam;
    }
}
