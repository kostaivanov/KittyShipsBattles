using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovingState : PlayerBaseState
{
    internal bool isMoving = false;
    public const float movementThreshold = 0.1f;
    private Vector3 previousPosition;

    internal override void EnterState(PlayerStateManager player)
    {
        player.animator.Play("Moving_P2");
        isMoving = true;
        //player.mouseInputManager.allowedToShoot = false;

        //previousPosition = player.gameObject.transform.position;
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        // Move player to the clicked position
        player.selectedPlayer.MovePlayer(player.mouseInputManager.clickedWorldPosition);

        MouseInputManager mouseInputManager = player.GetComponent<MouseInputManager>();

        if (mouseInputManager.isDragging(player) == 0)
        {
            Debug.Log("we are in moving stage here");

            player.selectedPlayer.MovePlayer(player.mouseInputManager.clickedWorldPosition);
        }

        // Vector3 positionChange = player.gameObject.transform.position - previousPosition;
        player.trajectoryLine.EndLine();
        if (player.gameObject.transform.position.Equals(previousPosition))
        {
            player.mouseInputManager.dragDistance = -1;
            player.SwitchState(player.selectedState);
            //player.mouseInputManager.allowedToShoot = true;
            player.mouseInputManager.initialMousePosition = player.gameObject.transform.position;
            isMoving = false;
        }
        previousPosition = player.gameObject.transform.position;



    }
}
