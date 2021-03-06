﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce lo stato di morte del Boss
/// </summary>
public class Boss2DeathState : Boss2StateBase
{
    /// <summary>
    /// Riferimento al boss controller
    /// </summary>
    private Boss2Controller bossCtrl;

    public override void Enter()
    {
        bossCtrl = context.GetBossController();
        bossCtrl.KillBoss();
    }
}
