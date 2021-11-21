using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObrigadoScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private FadeImage fadeScript;
    [SerializeField] private BoxCollider col;

    private void Start()
    {
        col.enabled = false;
        GameManager.instance.endDemoEvent += EnableCollider;
    }

    private void EnableCollider()
    {
        col.enabled = true;
    }
    public void Fade()
    {
        fadeScript.SetFadeIn(true);
        fadeScript.SetHasNextFade(false);
        fadeScript.SetHasSceneLoad(true);
        fadeScript.SetSceneIndex(sceneIndex);

        fadeScript.StartCoroutine(fadeScript.Fade(3f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fade();
        }
    }


}
