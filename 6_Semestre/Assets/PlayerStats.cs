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
    private Animator animator;

    //Backing Variables
    [Header("String Save Path")]
    [SerializeField] private string _savePath;

    [Header("Stats Values")]
    [SerializeField] private float _maxInsanity;
    [SerializeField] private float _maxFuel;
    [SerializeField] private float _currentInsanity;
    [SerializeField] private float _currentFuel;
    

    //Encapsulated Variables
    public float currentInsanity { get => _currentInsanity; set => _currentInsanity = value; }
    public float currentFuel { get => _currentFuel; set => _currentFuel = value; }
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

    public bool isUsingLamparina;

    private void Awake()
    {
        playerDataObject.Load();

    }

    private void Start()
    {
        _maxInsanity = playerDataObject.playerData.maxInsanity;
        _maxFuel = playerDataObject.playerData.maxFuel;

        currentInsanity = playerDataObject.playerData.currentInsanity;
        currentFuel = playerDataObject.playerData.currentFuel;

        animator = GetComponent<Animator>();
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
            playerDataObject.playerData.currentInsanity = this.currentInsanity;
            playerDataObject.playerData.currentFuel = this.currentFuel;
            playerDataObject.Save();
            save = false;
        }

        animator.SetBool("isUsingLamparina", isUsingLamparina);
        //Debug.Log(currentInsanity + " " + currentFuel);
    }

    public void UpdateStatsUI()
    {
        insanityBar.fillAmount = currentInsanity / 100f;
        fuelBar.fillAmount = currentFuel / 100f;
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
        currentInsanity -= damage;
        postprocessingControl.espiritualEnergy = currentInsanity;
        postprocessingControl.UpdatePPEE();

        if (currentInsanity <= 0)
        {
            Fade();
            currentInsanity = 0;
        }
    }

    public void Heal(int healingAmount)
    {
        currentInsanity += healingAmount;
        if (currentInsanity >= _maxInsanity) currentInsanity = _maxInsanity;
        postprocessingControl.espiritualEnergy = currentInsanity;
        postprocessingControl.UpdatePPEE();
    }

    public void FillLamparina(float fillAmount)
    {
        currentFuel += fillAmount;
        if (currentFuel >= _maxFuel) currentFuel = _maxInsanity;
    }
}
