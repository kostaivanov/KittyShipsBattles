using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MoveOrShootState : PlayerBaseState
{
    #region Shooting/Moving variables
    internal Vector3 initialMousePosition;

    //internal float mouseDownTime;

    internal float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag

    #endregion

    internal override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entered the Move or shoot state! - " + player.gameObject.name);
    }

    internal override void UpdateState(PlayerStateManager player)
    {
        //Debug.Log("Update the Move or shoot state!");
        // Get mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (player.selectedPlayer.name == player.gameObject.name && player.selectedState.selected == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = mousePosition;
                //mouseDownTime = Time.time;
            }

            // Detect mouse button release and shoot projectile
            else if (Input.GetMouseButtonUp(0))
            {
                //float clickDuration = Time.time - mouseDownTime;
                //Vector3 mouseUpPosition = Input.mousePosition;
                Vector3 clickedWorldPosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickedWorldPosition.z = 0;

                float dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);
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

                if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    //Debug.Log("drag distance - " + dragDistance);
                    if (dragDistance > dragThreshold)
                    {
                        player.projectileController.ShootProjectile(initialMousePosition, clickedWorldPosition, initialSpeed);
                    }
                    else
                    {
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
