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

    private void Update()
    {
        UpdateUI();
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            espiritualEnergy -= 1;
            postprocessingControl.espiritualEnergy = espiritualEnergy;
            postprocessingControl.UpdatePPEE();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            espiritualEnergy += 1;
            postprocessingControl.espiritualEnergy = espiritualEnergy;
            postprocessingControl.UpdatePPEE();
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
}
