using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlaySound : MonoBehaviour, ISound
{
    [Header("Sound settings")]
    [Tooltip("The name of the path sound from FMOD project")]
    [SerializeField] private string _soundPath;

    [Header("3D settings")]
    [Tooltip("It is a 3D sound?")]
    [SerializeField] private bool _is3dSound;
    [SerializeField] private Transform _soundSource;

    [Header("Play Settings")]
    [SerializeField] private bool playOnStart;
    [SerializeField][Range(0, 5)] private float delayTime;

    private FMOD.Studio.EventInstance _sound;

    public string soundPath { get => _soundPath; set => _soundPath = value; }
    public bool is3Dsound { get => _is3dSound; set => _is3dSound = value; }

    private void Awake()
    {
        if (_is3dSound)
        {
            if (_soundSource == null)
            {
                _sound = FMODUnity.RuntimeManager.CreateInstance(_soundPath);
                _soundSource = this.gameObject.transform;
                _sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_soundSource));
            }   
        }
    }

    private void Start()
    {
        if(playOnStart)
        {
            if(is3Dsound)
            {
                Invoke("PlayOneShoot3D", delayTime);
            }
            else
            {
                Invoke("PlayOneShoot2D", delayTime);
            }
        }
    }

    private void OnEnable()
    {
        if (_is3dSound)
        {
            _sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_soundSource));
        }
    }

    private void OnDisable()
    {
        StopSound();
    }

    private void Update()
    {
        if (_is3dSound)
        {
            _sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_soundSource));
        }
    }

    public void StartSound()
    {
        _sound.start();
    }

    public void StopSound()
    {
        _sound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        _sound.release();
    }

    public void PlayOneShoot2D()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_soundPath);
    }

    public void PlayOneShoot3D()
    {
        FMODUnity.RuntimeManager.PlayOneShot(soundPath, _soundSource.transform.position);
    }
}