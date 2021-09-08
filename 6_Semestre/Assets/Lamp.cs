using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    #region Summary
    // ~For that script, we need to cast a spherical area around our character and get info about every ghost object in the zone.If the ghost is afraid of lights,
    // it will go into the HIDING state, but if the ghost is attracted to light, it will change to the CHASE state.
    // also, when the player has the lamp activated, a lightsource is enabled, making it easier to see stuff in the dark. 
    // The lamp however have a limited time use, and will eventually need to be recharged with OIL.
    #endregion

    [SerializeField] private Light pointLight;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
