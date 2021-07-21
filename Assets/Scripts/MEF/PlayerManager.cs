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
    private List<InteractionTrigger> interactTriggersInRange;

    private void Awake()
    {
        inputs = GetComponent<RewiredInputManager>();
        movement = GetComponent<PlayerMovement>();
        modelRenderer.SetActive(false);
        interactTriggersInRange = new List<InteractionTrigger>();
    }

    private void Update()
    {
        UpdateMovement();
        ShowDebugRenderer();
        InteractInput();
        //currentState?.Execute();
    }

    /*
    private void FixedUpdate()
    {
        currentState?.FixedExecute();
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<InteractionTrigger>()!=null)
        {
            interactTriggersInRange.Add(other.GetComponent<InteractionTrigger>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<InteractionTrigger>() != null)
        {
            interactTriggersInRange.Remove(other.GetComponent<InteractionTrigger>());
        }
    }

    private void InteractInput()
    {
        if(inputs.GetInteractInputDown())
        {
            foreach(var item in interactTriggersInRange)
            {
                item.OnPlayerInteractInput();
            }
        }
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

    public RewiredInputManager Input { get => inputs; }

    #endregion

}
