using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeToTelaObrigado : MonoBehaviour, IFade
{
    [SerializeField] private FadeImage fadeScript;
    [SerializeField] private int sceneToLoadIndex;
    public void Fade()
    {
        fadeScript.SetFadeIn(true);
        fadeScript.SetHasNextFade(false);
        fadeScript.SetHasSceneLoad(true);
        fadeScript.SetSceneIndex(sceneToLoadIndex);

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
