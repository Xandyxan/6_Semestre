using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [Header("Auto Start?")]
    [SerializeField] private bool start;

    [Header("Duration of fade")]
    [SerializeField] private float _durationTime;
    [SerializeField] private float _delayTime;

    [Header("Color fade")]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _endColor;
    [SerializeField] private FadeType _fadeType;

    private Image imagem;

    [Header("Scene Management")]
    [SerializeField] private bool _loadScene;
    [SerializeField] private int _sceneIndex;

    private void Awake()
    {
        imagem = GetComponent<Image>();

        ChooseFadeType(_fadeType);

        imagem.color = _startColor;
    }

    private void Start()
    {
        if(start) StartCoroutine(FadeFunction(_durationTime, FadeType.Out));
    }

    public IEnumerator FadeFunction(float tempoFade, FadeType fade)
    {
        ChooseFadeType(fade);
        yield return new WaitForSeconds(_delayTime);
        float t = 0;
        while (_startColor != null)
        {
            t += 1 * Time.deltaTime;
            float normalizedTime = t / tempoFade;

            imagem.color = Color.Lerp(_startColor, _endColor, normalizedTime);
            yield return null;
        }
        
        imagem.color = _endColor;

        if (_loadScene)
        {
            GameManager.instance.LoadScene(_sceneIndex); 
        }
    }

    public void ChooseFadeType(FadeType fadeType)
    {
        if (fadeType == FadeType.In)
        {
            _startColor = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 0);
            _endColor = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 1);
            imagem.color = _startColor;
        }
        else if (fadeType == FadeType.Out)
        {
            _startColor = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 1);
            _endColor = new Color(imagem.color.r, imagem.color.g, imagem.color.b, 0);
            imagem.color = _startColor;
        }
    }

}

public enum FadeType
{
    Out,
    In
}
