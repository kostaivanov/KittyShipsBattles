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
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.selectedPlayer.name == player.gameObject.name && player.idleState.selected == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = mousePosition;
            }

            // Detect mouse button release and shoot projectile
            else if (Input.GetMouseButtonUp(0))
            {
                Vector3 clickedWorldPosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickedWorldPosition.z = 0;

                float dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);


                // Get mouse position in world coordinates
                Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

                if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    //Debug.Log("drag distance - " + dragDistance);
                    if (dragDistance > dragThreshold)
                    {
                        player.SwitchState(player.shootingState);
                    }
                    else
                    {
                        player.SwitchState(player.movingState);
                        player.selectedPlayer.MovePlayer(clickedWorldPosition);
                    }
                }

                player.trajectoryLine.EndLine();
            }

            if (Input.GetMouseButton(0))
            {
                //Vector3 currentPoint = mousePosition;
                player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
            }
        }
    }
}
