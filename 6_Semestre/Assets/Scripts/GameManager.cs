using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // for now, this script calls events that remove player movement or pause the game, there's also a method for setting the cursor on and off.

    #region ---Singleton Stuff---
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }
    #endregion

    #region ---Delegates---
    public delegate void TakePlayerControl();               // tira controle da movimenta��o do player.
    public TakePlayerControl removePlayerControlEvent;
    public delegate void ReturnPlayerControl();             // retorna o controle do player sobre sua movimenta��o.
    public ReturnPlayerControl returnPlayerControlEvent;

    public delegate void PauseGameTrue();                       // pausa o jogo
    public PauseGameTrue pauseGameTrue;
    public delegate void PauseGameFalse();
    public PauseGameFalse pauseGameFalse;
    #endregion

    [Header("Pause Menu Screens")]
    [SerializeField] private GameObject pauseMenuObject;
    [HideInInspector] public bool isPausedGame;
    //[SerializeField] private GameObject homePauseMenu;
    [SerializeField] private GameObject[] secondaryPauseMenus;

    [Header("It's main screen?")]
    [SerializeField] private bool mainMenuScreen;

   // [Header("Tasks")]
   // [SerializeField] private Text taskText; // ainda n�o sei se vamos colocar alguma forma de task nesse jogo

    private bool playerWasNotFree = false;
    private bool usingInventory, playerInScene; // pensei em utilizar o mesmo m�todo que estavamos usando pra tratar a intera��o com o celular, s� que com o invent�rio
    [SerializeField] private PlayerController3rdPerson playerMovement;

    //----------------------------------------------------------------------------\\

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
        // print(_instance.name);

        if (!mainMenuScreen) SetLockCursor(true);
        if (playerMovement == null) playerInScene = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !mainMenuScreen)
        {
            if (!isPausedGame) SetPauseGame(true);
            else SetPauseGame(false);
        }
       
    }

    public void SetLockCursor(bool on)
    {
        if (on)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void SetPauseGame(bool on)
    {
        if (on)
        {
            if (playerInScene)
            {
                if (!playerMovement.GetCanMove()) playerWasNotFree = true;
               // usingInventory = playerMovement.GetUsingInventory();
            }
            Time.timeScale = 0;
            isPausedGame = true;
            SetLockCursor(false);
            pauseMenuObject.SetActive(true);
           // homePauseMenu.SetActive(true);
            

            instance.pauseGameTrue?.Invoke();
            instance.removePlayerControlEvent();
            //if (Inventory.instance != null) Inventory.instance.SetIsPausedGame(true);
        }
        else
        {
            Time.timeScale = 1;
            isPausedGame = false;
            SetLockCursor(true);
            for (int i = 0; i < secondaryPauseMenus.Length; i++) secondaryPauseMenus[i].SetActive(false);
            pauseMenuObject.SetActive(false);

            instance.pauseGameFalse?.Invoke();
            if (!playerWasNotFree)
            {
                instance.returnPlayerControlEvent();
            }
           // if (!usingInventory)
           // {
                
           // }

           // if (Inventory.instance != null) Inventory.instance.SetIsPausedGame(false);
            playerWasNotFree = false;
        }
    }

    #region ---Scene Management---
    public void PlayGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    public void LoadScene(int sceneNumber) { SceneManager.LoadScene(sceneNumber); }
    public void BacktoMenu() { SceneManager.LoadScene(0); }
    public void QuitGame() { Application.Quit(); }
    #endregion

    public void SetPrefValue(string prefKey, int number)     // -> Pode ser util em algum momento
    {
        PlayerPrefs.SetInt(prefKey, number);
    }

    public void ClearProgress()
    {
        PlayerPrefs.SetInt("levelAt", 2);
    }
/*
    public void SetTaskText(string actualTask)  // scripts v�o chamar pra colocar um "Pegue uma toalha" aparecendo no canto da tela
    {
        taskText.text = actualTask;
        taskText.gameObject.SetActive(true);
    }

    public void ConcludeCurrentTask()    // quando uma condi��o de task acaba, o script da task chama esse m�todo
    {
        taskText.text = "";
        taskText.gameObject.SetActive(false);
    }
*/
}
