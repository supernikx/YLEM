﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe che gestisce le distanze dell'agent
/// </summary>
public class AgentDistanceController : MonoBehaviour
{
    [Header("Agent Group Settings")]
    //Range alla distanza di raggruppamento del gruppo
    [SerializeField]
    private Vector2 regroupDistanceRange;
    //Range alla distanza di espansione del gruppo
    [SerializeField]
    private Vector2 expandDistanceRange;

    [Header("Obstacles Settings")]
    //Layer degli ostacoli
    [SerializeField]
    private LayerMask obstaclesLayer;
    //Lunghezza del ray per controllare gli ostacoli
    [SerializeField]
    private float obstacleCheckRayLenght;

    /// <summary>
    /// Riferimento all'agent controller
    /// </summary>
    private AgentController agentCtrl;
    /// <summary>
    /// Distanza di espansione
    /// </summary>
    private float expandDistance;
    /// <summary>
    /// Distanza di raggruppamento
    /// </summary>
    private float regroupDistance;

    /// <summary>
    /// Funzione che setup
    /// </summary>
    public void Setup(AgentController _agentCtrl)
    {
        agentCtrl = _agentCtrl;

        CalculateDistances();
    }

    /// <summary>
    /// Funzione che calcola dei valori random per il raggruppamento e l'espansione
    /// </summary>
    public void CalculateDistances()
    {
        expandDistance = UnityEngine.Random.Range(expandDistanceRange.x, expandDistanceRange.y);
        regroupDistance = UnityEngine.Random.Range(regroupDistanceRange.x, regroupDistanceRange.y);
    }

    /// <summary>
    /// Funzione che ritorna la distanza da tenere quando il gruppo è in raggruppamento
    /// </summary>
    /// <returns></returns>
    public float GetRegroupDistance()
    {
        return regroupDistance;
    }

    /// <summary>
    /// Funzione che ritorna la distanza da tenere quando il gruppo è in espansione
    /// </summary>
    /// <returns></returns>
    public float GetExpandDistance()
    {
        return expandDistance;
    }

    /// <summary>
    /// Funzione che controlla se ci sono ostacoli nel tragitto di espansione
    /// </summary>
    /// <param name="_groupCenter"></param>
    public bool CheckExpandDistance(Vector3 _groupCenter)
    {
        Vector3 rayDirection = (transform.position - _groupCenter).normalized;
        rayDirection.y = 0;
        Ray ray = new Ray(transform.position, rayDirection);

        if (Physics.Raycast(ray, obstacleCheckRayLenght, obstaclesLayer))
            return false;

        Debug.DrawRay(transform.position, rayDirection * obstacleCheckRayLenght, Color.red);
        return true;
    }

    /// <summary>
    /// Funzione che controlla se ci sono ostacoli nel tragitto di raggruppamento
    /// </summary>
    /// <param name="_groupCenter"></param>
    public bool CheckRegroupDistance(Vector3 _groupCenter)
    {
        Vector3 rayDirection = (_groupCenter - transform.position).normalized;
        rayDirection.y = 0;
        Ray ray = new Ray(transform.position, rayDirection);

        if (Physics.Raycast(ray, obstacleCheckRayLenght, obstaclesLayer))
            return false;

        Debug.DrawRay(transform.position, rayDirection * obstacleCheckRayLenght, Color.blue);
        return true;
    }
}