using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerSelectedState : PlayerBaseState
{
    internal Vector3 initialMousePosition;
    internal float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag

    internal override void EnterState(PlayerStateManager player)
    {
        
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        // Get mouse position in world coordinates
        if (player.selectedPlayer.name == player.gameObject.name && player.idleState.selected == true)
        {
            MouseInputManager mouseInputManager = player.GetComponent<MouseInputManager>();
            //Debug.Log("drag distance - " + dragDistance);
            if (mouseInputManager.isDragging(player) == 1)
            {
                player.SwitchState(player.shootingState);
            }
            else if(mouseInputManager.isDragging(player) == 0)
            {
                player.SwitchState(player.movingState);
            }              
        }
    }
}
