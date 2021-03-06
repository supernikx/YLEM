﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce lo sparo del Boss
/// </summary>
public class Boss1ShootController : MonoBehaviour
{
    [Header("Shoot Settings")]
    [SerializeField]
    private List<Transform> shootPointsParent;

    /// <summary>
    /// Riferimento al BossController
    /// </summary>
    private Boss1Controller bossCtrl;

    /// <summary>
    /// Funzione di Setup
    /// </summary>
    /// <param name="_bossCtrl"></param>
    public void Setup(Boss1Controller _bossCtrl)
    {
        bossCtrl = _bossCtrl;
    }

    #region API
    /// <summary>
    /// Funzione che spara un proiettile per ogni shoot point
    /// </summary>
    public void Shoot(int _shootPointIndex)
    {
        _shootPointIndex = Mathf.Clamp(_shootPointIndex, 0, shootPointsParent.Count - 1);
        if (bossCtrl.IsSetuppedAndEnabled())
        {
            foreach (Transform point in shootPointsParent[_shootPointIndex])
            {
                Boss1BulletController newBullet = PoolManager.instance.GetPooledObject(ObjectTypes.Boss1Bullet, gameObject).GetComponent<Boss1BulletController>();
                if (newBullet != null)
                {
                    newBullet.transform.SetPositionAndRotation(point.position, Quaternion.LookRotation(point.forward.normalized));
                    newBullet.Setup();
                }
            }
        }
    }
    #endregion
}
