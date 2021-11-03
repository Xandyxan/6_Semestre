using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private PlayerDataObject playerDataObject;

    //Backing Variables
    [Header("String Save Path")]
    [SerializeField] private string _savePath;

    
    [SerializeField] private float _insanityValue;
    [SerializeField] private float _lamparinaFuel;
    

    //Encapsulated Variables
    public float insanityValue { get => _insanityValue; set => _insanityValue = value; }
    public float lamparinaFuel { get => _lamparinaFuel; set => _lamparinaFuel = value; }
    public string savePath { get => _savePath; set => _savePath = value; }

    [Header("Just for Debug")]
    public bool load;
    public bool save;

    [Header("Visual UI")]
    [SerializeField] private FadeImage fadeScript;
    [SerializeField] private Image insanityBar;
    [SerializeField] private Image fuelBar;

    [Header("Post Processing")]
    [SerializeField] private PostprocessingControlTest postprocessingControl;

    [Header("Others")]
    [SerializeField] private int sceneToLoadIndex = 3;

    private void Awake()
    {
        playerDataObject.Load();

    }

    private void Start()
    {
        this.insanityValue = playerDataObject.playerData.health;
        this.lamparinaFuel = playerDataObject.playerData.lamparinaFuel;
    }

    void Update()
    {
        UpdateStatsUI();

        if (load)
        {
            playerDataObject.Load();
            //this.health = playerDataObject.playerData.health;                     //It is on the Start so whatever
            //this.lamparinaFuel = playerDataObject.playerData.lamparinaFuel;
            load = false;
        }

        if (save)
        {
            playerDataObject.playerData.health = this.insanityValue;
            playerDataObject.playerData.lamparinaFuel = this.lamparinaFuel;
            playerDataObject.Save();
            save = false;
        }

        Debug.Log(insanityValue + " " + lamparinaFuel);
    }

    public void UpdateStatsUI()
    {
        insanityBar.fillAmount = insanityValue / 100f;
        fuelBar.fillAmount = lamparinaFuel / 100f;
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
        insanityValue -= damage;
        postprocessingControl.espiritualEnergy = insanityValue;
        postprocessingControl.UpdatePPEE();

        if (insanityValue <= 0)
        {
            Fade();
            insanityValue = 0;
        }
    }

    public void Heal(int healingAmount)
    {
        insanityValue += healingAmount;
        if (insanityValue >= 100) insanityValue = 100;
        postprocessingControl.espiritualEnergy = insanityValue;
        postprocessingControl.UpdatePPEE();
    }
}
