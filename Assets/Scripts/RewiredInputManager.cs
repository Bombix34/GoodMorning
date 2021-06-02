using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class RewiredInputManager : MonoBehaviour
{
    // The Rewired player id of this character
    public int playerId = 0;

    private Player player; // The Rewired Player

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);
    }


    #region MOVEMENT_INPUT

    public float GetMovementInputX()
    {
        return player.GetAxis("Move Horizontal");
    }

    public float GetMovementInputY()
    {
        return player.GetAxis("Move Vertical");
    }

    public Vector3 GetMovementInput()
    {
        Vector3 move = Vector3.zero;
        move.x = player.GetAxis("Move Horizontal");
        move.z = player.GetAxis("Move Vertical");
        return move;
    }

    #endregion


    #region BUTTONS_INPUT

    //Interact
    public bool GetInteractInputDown(int index=1)
    {
        return player.GetButtonDown("Interact_"+index);
    }

    public bool GetInteractInput(int index = 1)
    {
        return player.GetButton("Interact_" + index);
    }

    public bool GetInteractInputUp(int index = 1)
    {
        return player.GetButtonUp("Interact_" + index);
    }

    //Start
    public bool GetStartInput()
    {
        return player.GetButtonDown("Start");
    }
    public bool GetStartInputUp()
    {
        return player.GetButtonUp("Start");
    }

    //Any
    public bool GetPressAnyButtonDown()
    {
        return player.GetAnyButtonDown();
    }

    #endregion

}
