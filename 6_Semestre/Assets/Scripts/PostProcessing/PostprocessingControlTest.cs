using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostprocessingControlTest : MonoBehaviour
{
    public Volume volume;
    ColorAdjustments colorAdjustments;
    Vignette vignette;
    WhiteBalance whiteBalance;

    public int espiritualEnergy = 100;


    private void Update()
    {
        if (espiritualEnergy <= 0) espiritualEnergy = 0;
        else if (espiritualEnergy >= 100) espiritualEnergy = 100;

        if (Input.GetKeyDown(KeyCode.I))
        {
            espiritualEnergy -= 1;
            UpdatePPEE();
        }else if (Input.GetKeyDown(KeyCode.P))
        {
            espiritualEnergy += 1;
            UpdatePPEE();
        }
        if(espiritualEnergy <= 0)
        {
            print("GAME OVER!!!!!!!!!!!!!!!!!!!!");
        }
    }

    public void UpdatePPEE()
    {
        int eeLost = Mathf.Abs(100 - espiritualEnergy); // 100 -> 0, 30 -> 70, 0 -> 100...
        // ee = 100 -> -4 => saturation = -4 / contrast = +4. 
        // ee = 96 -> +2 => saturation = -2 / contrast = +2

        if (volume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.saturation.value = 0;
            colorAdjustments.contrast.value = 0;   

            colorAdjustments.saturation.value -= eeLost; // starts at 0, decreases by the ee points lost
            colorAdjustments.contrast.value += eeLost;   // starts at 0, increases by the ee points lost
        }
        if(volume.profile.TryGet<Vignette>(out vignette))
        {
            // range between 0(fine) and 0.5(crazy) / 100 ee -> 0 vi, 0 ee -> 0.5f vi / 70 ee -> x vi
            vignette.intensity.value = 0;
            vignette.intensity.value += (eeLost / 200f); // starts at 0, stops at 0.5

        }

        if(volume.profile.TryGet<WhiteBalance>(out whiteBalance))
        {
            whiteBalance.temperature.value = 0;
            whiteBalance.temperature.value -= eeLost; // starts at 0, decreases by the ee points lost
        }
    }

}
