using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MoveOrShootState : PlayerBaseState
{
    #region Shooting/Moving variables
    internal Vector3 initialMousePosition;

    internal float mouseDownTime;
    internal float maxClickDuration = 0.1f; // Maximum time the mouse button can be held down for a click to be registered
    internal float minDragDistance = 0.5f; // Maximum time the mouse button can be held down for a click to be registered

    internal bool isDragging = false;
    private const float dragDelay = 0.05f;
    private float dragStartTime;
    #endregion

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
                initialMousePosition = mousePosition;
                isDragging = false;
                mouseDownTime = Time.time;
                dragStartTime = Time.time;
            }

            // Detect mouse button release and shoot projectile
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                float clickDuration = Time.time - mouseDownTime;
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


                if (isDragging)
                {
                    // Shoot the projectile
                    player.projectileController.ShootProjectile(initialMousePosition, clickedWorldPosition, initialSpeed);
                }
                else if(clickDuration < maxClickDuration && dragDistance < minDragDistance &&
                viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    // Move player to the clicked position
                    player.selectedPlayer.MovePlayer(clickedWorldPosition);
                }

                //if (clickDuration < maxClickDuration && dragDistance < minDragDistance &&
                //viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                //{
                //    // Move player to the clicked position
                //    player.selectedPlayer.MovePlayer(clickedWorldPosition);
                //}
                //else
                //{

                //}

                isDragging = false;
                player.trajectoryLine.EndLine();
            }

            if (Input.GetMouseButton(0))
            {
                if (!isDragging && Time.time - dragStartTime >= dragDelay)
                {
                    isDragging = true;
                }
                if (isDragging)
                {
                    Vector3 currentPoint = mousePosition;
                    player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
                }
            }
        }
    }
}
