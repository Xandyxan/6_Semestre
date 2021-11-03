using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnInteraction : MonoBehaviour, IInteractable, IFade, ISelectable
{
    [Header("Scene Index")]
    [SerializeField] int sceneToLoadIndex;

    [Header("Position on Scene transition")]
    [Tooltip("A posição em que o player começa ao carregar outra cena. Ex: outro lado de uma porta, etc")]
    [SerializeField] float transPosX;
    [SerializeField] float transPosY;
    [SerializeField] float transPosZ;

    [Header("Fade")]
    [SerializeField] private FadeImage fadeScript;

    [Header("Selection")]
    [Tooltip("The visual feedback for an item that can be interacted with")]
    [SerializeField] private GameObject interactionFeedback;

   
    public void Fade()
    {
        fadeScript.SetFadeIn(true);
        fadeScript.SetHasNextFade(false);
        fadeScript.SetHasSceneLoad(true);
        fadeScript.SetSceneIndex(sceneToLoadIndex);

        fadeScript.StartCoroutine(fadeScript.Fade(3f));
    }

    public void Interact()
    {
        // registra gaveta com o index dessa porta
        // popula a gaveta com os valores da posição
        SetSceneStartPos();
        Fade();
    }

    public void Select()
    {
        interactionFeedback.SetActive(true);
    }

    public void Deselect()
    {
        interactionFeedback.SetActive(false);
    }

    private void SetSceneStartPos()
    {
        PlayerPrefs.SetFloat("PlayerStartPosX", transPosX);
        PlayerPrefs.SetFloat("PlayerStartPosY", transPosY);
        PlayerPrefs.SetFloat("PlayerStartPosZ", transPosZ);
    }
}
