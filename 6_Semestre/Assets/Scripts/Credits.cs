using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public float speed;
    private float actualSpeed, normalSpeed, doubleSpeed;
    public float endPoint;     //-55.87f
    [SerializeField] private int sceneToload;

    private RectTransform rectTransform;
    

    private void Awake()
    {
        normalSpeed = speed;
        doubleSpeed = normalSpeed * 5f;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Stationary)
            {
                actualSpeed = doubleSpeed; 
            }
        }
        else if(Input.touchCount == 0)
        {
            actualSpeed = normalSpeed;
        }

        if (rectTransform.anchoredPosition.y < endPoint) 
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - actualSpeed);
        }
        else if(rectTransform.anchoredPosition.y >= endPoint)
        { 
            Invoke("LoadScene", 2.5f); 
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToload);
    }
}
