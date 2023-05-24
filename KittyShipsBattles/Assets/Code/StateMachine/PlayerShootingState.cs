using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerShootingState : PlayerBaseState
{
    internal override void EnterState(PlayerStateManager player)
    {
        throw new System.NotImplementedException();
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.selectedPlayer.name == player.gameObject.name)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.initialMousePosition = mousePosition;
                player.isDragging = true;
                player.mouseDownTime = Time.time;
            }

            // Detect mouse button release and shoot projectile
            else if (Input.GetMouseButtonUp(0) && player.isDragging)
            {
                float clickDuration = Time.time - player.mouseDownTime;
                //Vector3 mouseUpPosition = Input.mousePosition;
                Vector3 clickedWorldPosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickedWorldPosition.z = 0;

                float dragDistance = Vector2.Distance(player.initialMousePosition, clickedWorldPosition);
                float initialSpeed = player.projectileController.CalculateShootingPower(dragDistance);

                //Debug.Log("mouseDownTime: " + mouseDownTime);
                //Debug.Log("Time.time: " + Time.time);
                //Debug.Log("clickDuration: " + clickDuration);
                //Debug.Log("drag distance: " + dragDistance);
                //Debug.Log("minDragDistance " + minDragDistance);

                // Get mouse position in world coordinates
                Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

                //Debug.Log("Distance between start point and last point of the mouse: " + Vector2.Distance(initialMousePosition, mousePosition));
                //Debug.Log("max Distance: " + maxDistance);
                //Debug.Log("shootingPower: " + shootingPower);
                //Debug.Log("Mathf.Lerp of initialSpeed: " + initialSpeed);

                if (clickDuration < player.maxClickDuration && dragDistance < player.minDragDistance &&
                viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    Debug.Log("Entering?");
                    // Move player to the clicked position
                    player.selectedPlayer.MovePlayer(clickedWorldPosition);
                }
                else
                {
                    // Shoot the projectile
                    player.projectileController.ShootProjectile(player.initialMousePosition, clickedWorldPosition, initialSpeed);
                }

                player.isDragging = false;
                player.trajectoryLine.EndLine();
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = mousePosition;
                player.trajectoryLine.RenderLine(player.initialMousePosition, mousePosition);
            }
        }
       
    }
}
