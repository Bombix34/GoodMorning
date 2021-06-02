using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rigidBody;
    private bool canMove = true;
    private Vector3 currentVelocity;

    public bool canMoveInDepth = true;
    public float moveSpeed = 1f;

    //to apply a modif to the rotation speed
    private float modifRotationSpeed = 1f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void DoMove(Vector3 playerInputs)
    {
        if (!canMove)
        {
            return;
        }
        Vector3 directionController = playerInputs;
        if (directionController == Vector3.zero)
        {
            currentVelocity = Vector3.zero;
            return;
        }
        //init values
        Vector3 forward =Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        Vector3 right = Camera.main.transform.right;

        Vector3 rightMove = right * (10 * playerInputs.x) * Time.deltaTime;
        Vector3 upMove =  forward * (10 * playerInputs.z) * Time.deltaTime;
        Vector3 heading = canMoveInDepth ? (rightMove + upMove).normalized : rightMove.normalized;

        float amplitude = new Vector2(playerInputs.x, canMoveInDepth ? playerInputs.z : 0f).magnitude;

        currentVelocity = Vector3.zero;
        currentVelocity += heading * amplitude * (moveSpeed);
        rigidBody.MovePosition(transform.position + currentVelocity);
    }

    public void ResetVelocity()
    {
        DoMove(Vector3.zero);
    }


    #region GET/SET

    public Vector3 CurrentVelocity
    {
        get => currentVelocity;
        set
        {
            currentVelocity = value;
        }
    }

    public Rigidbody Body
    {
        get => rigidBody;
    }

    public bool CanMove
    {
        get => canMove;
        set
        {
            canMove = value;
        }
    }

    public Vector3 GetHeadingDirection()
    {
        return transform.TransformDirection(Vector3.forward);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(GetGroundedRay(), 1.05f);
    }

    public Ray GetGroundedRay()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        return ray;
    }

    #endregion
}
