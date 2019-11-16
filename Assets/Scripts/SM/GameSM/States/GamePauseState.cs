﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce lo stato di Pause della GameSM
/// </summary>
public class GamePauseState : GameSMStateBase
{
    /// <summary>
    /// Riferimento all'UI Manager
    /// </summary>
    private UI_Manager uiMng;
    /// <summary>
    /// Riferimento al pannello di pausa
    /// </summary>
    private UIMenu_Pause uiPausePanel;
    /// <summary>
    /// Riferimento al level manager
    /// </summary>
    private LevelManager lvlMng;
    /// <summary>
    /// Riferimento al level pause controller
    /// </summary>
    private LevelPauseController lvlPauseCtrl;
    /// <summary>
    /// Riferiemento al group controller
    /// </summary>
    private GroupController groupCtrl;

    public override void Enter()
    {
        Time.timeScale = 0;

        uiMng = context.GetGameManager().GetUIManager();
        lvlMng = context.GetGameManager().GetLevelManager();
        lvlPauseCtrl = lvlMng.GetLevelPauseController();
        groupCtrl = lvlMng.GetGroupController();
        uiPausePanel = uiMng.GetMenu<UIMenu_Pause>();

        lvlPauseCtrl.OnGameUnpause += HandleOnGameUnpause;
        uiPausePanel.ResumeButtonPressed += HandleOnGameUnpause;
        uiPausePanel.MainMenuButtonPressed += HandleOnMainMenuButtonPressed;

        groupCtrl.Enable(false);
        uiMng.SetCurrentMenu<UIMenu_Pause>();
    }

    #region Handles
    /// <summary>
    /// Funzione che gestisce l'evento di fine pausa
    /// </summary>
    private void HandleOnGameUnpause()
    {
        Complete();
    }

    /// <summary>
    /// Funzione che gestisce l'evento della pressione del botton MainMenu
    /// </summary>
    private void HandleOnMainMenuButtonPressed()
    {
        Complete(1);
    }
    #endregion

    public override void Exit()
    {
        if (lvlPauseCtrl != null)
            lvlPauseCtrl.OnGameUnpause -= HandleOnGameUnpause;

        if (uiPausePanel != null)
        {
            uiPausePanel.ResumeButtonPressed -= HandleOnGameUnpause;
            uiPausePanel.MainMenuButtonPressed -= HandleOnMainMenuButtonPressed;
        }

        if (groupCtrl != null)
            groupCtrl.Enable(true);

        Time.timeScale = 1;
    }
}