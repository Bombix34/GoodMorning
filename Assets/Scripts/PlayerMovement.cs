using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rigidBody;
    private bool canMove = true;
    private Vector3 currentVelocity;

    public PlayerMovementType movementType;
    public float moveSpeed = 1f;

    public float[] depthPosition;
    private int indexDepthPosition = 1;

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
        Vector3 forward = Vector3.forward ;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        Vector3 right = Vector3.right;

        Vector3 rightMove = right * (10 * playerInputs.x) * Time.deltaTime;
        Vector3 upMove =  forward * (10 * playerInputs.z) * Time.deltaTime;
        Vector3 heading = Vector3.zero;
        float amplitude = 0f;
        switch (movementType)
        {
            case PlayerMovementType.TWO_DIMENSIONS:
                heading = rightMove.normalized;
                amplitude = new Vector2(playerInputs.x,  0f).magnitude;
                break;
            case PlayerMovementType.THREE_DIMENSIONS:
                heading = (rightMove + upMove).normalized;
                amplitude = new Vector2(playerInputs.x, playerInputs.z).magnitude;
                break;
            case PlayerMovementType.TWO_DIMENSIONS_WITH_DEPTH:
                if(Mathf.Abs(playerInputs.z) > Mathf.Abs(playerInputs.x))
                {
                    Vector3 headingRaycast = playerInputs.z > 0f ? transform.TransformDirection(Vector3.forward) : transform.TransformDirection(Vector3.back);
                    RaycastHit hit;
                    if (!Physics.Raycast(transform.position, headingRaycast, out hit, 1))
                    {
                        canMove = false;
                        IndexDepthPosition = playerInputs.z > 0f ? indexDepthPosition + 1 : indexDepthPosition - 1;
                        transform.DOMoveZ(depthPosition[indexDepthPosition], 0.6f)
                            .OnComplete(()=> canMove=true);
                    }
                    return;
                }
                else
                {
                    heading = rightMove.normalized;
                    amplitude = new Vector2(playerInputs.x, 0f).magnitude;
                }
                break;
        }
        currentVelocity = Vector3.zero;
        currentVelocity += heading * amplitude * (moveSpeed);
        rigidBody.MovePosition(transform.position + currentVelocity);
    }

    public void ResetVelocity()
    {
        DoMove(Vector3.zero);
    }

    public void SwitchDepthPosition(bool isUp)
    {
    }

    private void OnDrawGizmos()
    {
        if(movementType == PlayerMovementType.TWO_DIMENSIONS_WITH_DEPTH)
        {
            for(int i = 0; i < depthPosition.Length; ++i)
            {
                Color color = new Color(1f, 0f, 0f, 0.4f);
                if (i == 1)
                    color = new Color(0f, 1f, 0f, 0.4f);
                else if (i == 2)
                    color = new Color(0f, 0f, 1f, 0.4f);
                Vector3 start = new Vector3(0f, -1f, depthPosition[i]);
                Gizmos.color = color;
                Gizmos.DrawCube(start, new Vector3(50f, 2f, 0.2f));
            }
        }
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

    public int IndexDepthPosition
    {
        get => indexDepthPosition;
        set
        {
            indexDepthPosition = value;
            if (indexDepthPosition >= depthPosition.Length)
            {
                indexDepthPosition = depthPosition.Length - 1;
                return;
            }
            if (indexDepthPosition <= 0)
            {
                indexDepthPosition = 0;
                return;
            }

        }
    }

    #endregion
}

public enum PlayerMovementType
{
    TWO_DIMENSIONS,
    THREE_DIMENSIONS,
    TWO_DIMENSIONS_WITH_DEPTH,
}
