using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lamp : MonoBehaviour
{
    #region Summary
    // ~For that script, we need to cast a spherical area around our character and get info about every ghost object in the zone.If the ghost is afraid of lights,
    // it will go into the HIDING state, but if the ghost is attracted to light, it will change to the CHASE state.
    // also, when the player has the lamp activated, a lightsource is enabled, making it easier to see stuff in the dark. 
    // The lamp however have a limited time use, and will eventually need to be recharged with OIL.
    #endregion

    // mudamos dinamicamente o tamanho do obstacle pra bater com o raio de alcance da luz.

    [SerializeField] private NavMeshObstacle litZone; 

    [SerializeField] private Light candleLight;

    [Header("Balancing")]
     private float lightMultiplier = 1f;
     private float obstacleMultiplier = 0.382f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { ChangeLightRadius(1f); }
        if(Input.GetKeyDown(KeyCode.J)) { ChangeLightRadius(-1f); }
    }

    private void ChangeLightRadius(float amount)
    {
        print("obstacle value:" + amount * obstacleMultiplier);
        print("light value:" + amount * lightMultiplier);
        candleLight.range = amount * lightMultiplier;
        litZone.radius = amount * obstacleMultiplier;
    }

    // light = light value - 10
    // collider = collider value - 3.82
}
