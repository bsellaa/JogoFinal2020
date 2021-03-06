﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Core;
using NavGame.Managers;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : TouchableGameObject
{
    NavMeshAgent agent;
    Camera cam;
    public LayerMask walkableLayer;
    public LayerMask collectibleLayer;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
