using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerManager : ObjectManager
{
    private RewiredInputManager inputs;
    private PlayerMovement movement;

    [SerializeField] private GameObject modelRenderer;

    private void Awake()
    {
        inputs = GetComponent<RewiredInputManager>();
        movement = GetComponent<PlayerMovement>();
        modelRenderer.SetActive(false);
    }

    private void Update()
    {
        UpdateMovement();
        ShowDebugRenderer();
        currentState?.Execute();
    }

    private void FixedUpdate()
    {
        currentState?.FixedExecute();
    }

    public override void ChangeState(State newState)
    {
        base.ChangeState(newState);
    }

    public void UpdateMovement()
    {
        movement.DoMove(inputs.GetMovementInput());
    }

    private void ShowDebugRenderer()
    {
        if(inputs.GetStartInput())
        {
            modelRenderer.SetActive(true);
        }
        else if(inputs.GetStartInputUp())
        {
            modelRenderer.SetActive(false);
        }
    }

    #region GET/SET

    public PlayerMovement Movement
    {
        get => movement;
    }

    #endregion

}
