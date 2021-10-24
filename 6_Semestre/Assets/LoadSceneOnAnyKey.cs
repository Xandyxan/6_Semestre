using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneOnAnyKey : MonoBehaviour
{
    [SerializeField] private float holdAmount = 2f;
    private float holdCounter = 0f;

    public Slider slider;
    private void Awake()
    {
        SetSliderMaxValue(holdAmount);
    }
    private void Update()
    {
        if (Input.anyKey)
        {
            holdCounter += Time.deltaTime;
            print(holdCounter);
            if(holdCounter >= holdAmount) GameManager.instance.LoadScene(0);
        }
        else
        {
            holdCounter -= Time.deltaTime;
            if (holdCounter <= 0) holdCounter = 0;
        }

        SetSliderValue(holdCounter);
    }

    public void SetSliderMaxValue(float mValue)
    {
        slider.maxValue = mValue;
        slider.value = 0;
    }
    public void SetSliderValue(float value)
    {
        slider.value = value;
    }
}
