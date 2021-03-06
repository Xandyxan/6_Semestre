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

    public float espiritualEnergy = 100;        //changed due PlayerStats, sorry :(


    private void Update()
    {
       // if (Input.GetKeyDown(KeyCode.I))
       // {
       //     espiritualEnergy -= 15;
       //     UpdatePPEE();
       // }else if (Input.GetKeyDown(KeyCode.O))
       // {
       //     espiritualEnergy += 15;
       //     UpdatePPEE();
       // }
        if(espiritualEnergy <= 0)
        {
            print("GAME OVER!!!!!!!!!!!!!!!!!!!!");
        }
    }

    public void UpdatePPEE() // postProcessing espiritual energy
    {
        if (espiritualEnergy <= 0) espiritualEnergy = 0;
        if (espiritualEnergy > 100) espiritualEnergy = 100;
        float eeLost = Mathf.Abs(100 - espiritualEnergy); // 100 -> 0, 30 -> 70, 0 -> 100...            //Change to float due PlayerStats
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
