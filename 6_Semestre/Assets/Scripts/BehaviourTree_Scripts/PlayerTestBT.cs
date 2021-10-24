using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTestBT : MonoBehaviour, IFade
{
    [SerializeField] private FadeImage fadeScript;
    [SerializeField] private int sceneToLoadIndex = 3;
    [SerializeField] private int espiritualEnergy = 100;

    [SerializeField] private PostprocessingControlTest postprocessingControl;

    [SerializeField] private Image fillSanidade;

    // testes de inventario
    [Space]
    [SerializeField] private GameObject uiInventario;
    private GameObject abaInventario;
    private GameObject abaArquivos;
    [SerializeField] private GameObject uiInGame;
    private bool inventarioAberto = false;
    private void Awake()
    {
        abaInventario = uiInventario.transform.GetChild(0).gameObject;
        abaArquivos = uiInventario.transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            espiritualEnergy -= 1;
            postprocessingControl.espiritualEnergy = espiritualEnergy;
            postprocessingControl.UpdatePPEE();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(15);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventarioAberto = !inventarioAberto;
            if (inventarioAberto)
            {
                GameManager.instance.usingInventory = true;
                uiInGame.SetActive(false);
                abaArquivos.SetActive(false);
                abaInventario.SetActive(true);
                uiInventario.SetActive(true);
                GameManager.instance.SetPauseGame(true, false);
            }
            else
            {
                GameManager.instance.usingInventory = false;
                uiInGame.SetActive(true);
                uiInventario.SetActive(false);
                GameManager.instance.SetPauseGame(false);
            }
        }
    }

    private void UpdateUI()
    {
        fillSanidade.fillAmount = espiritualEnergy / 100f;
    }

    public void Fade()
    {
        fadeScript.SetFadeIn(true);
        fadeScript.SetHasNextFade(false);
        fadeScript.SetHasSceneLoad(true);
        fadeScript.SetSceneIndex(sceneToLoadIndex);

        fadeScript.StartCoroutine(fadeScript.Fade(3f));
    }

    public void TakeDamage(int damage)
    {
        espiritualEnergy -= damage;
        postprocessingControl.espiritualEnergy = espiritualEnergy;
        postprocessingControl.UpdatePPEE();

        if(espiritualEnergy <= 0)
        {
            Fade();
            espiritualEnergy = 0;
        }
    }

    public void Heal(int healingAmount)
    {
        espiritualEnergy += healingAmount;
        if (espiritualEnergy >= 100) espiritualEnergy = 100;
        postprocessingControl.espiritualEnergy = espiritualEnergy;
        postprocessingControl.UpdatePPEE();
    }
}
