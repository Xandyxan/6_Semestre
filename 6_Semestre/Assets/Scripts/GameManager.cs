using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // for now, this script calls events that remove player movement or pause the game, there's also a method for setting the cursor on and off.

    #region ---Singleton Stuff---
    private static GameManager _instance;
    public static GameManager instance { get { return _instance; } }
    #endregion

    #region ---Delegates---
    public delegate void TakePlayerControl();               // tira controle da movimentação do player.
    public TakePlayerControl removePlayerControlEvent;
    public delegate void ReturnPlayerControl();             // retorna o controle do player sobre sua movimentação.
    public ReturnPlayerControl returnPlayerControlEvent;

    public delegate void PauseGameTrue();                       // pausa o jogo
    public PauseGameTrue pauseGameTrue;
    public delegate void PauseGameFalse();
    public PauseGameFalse pauseGameFalse;
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
        print(_instance.name);
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
}
