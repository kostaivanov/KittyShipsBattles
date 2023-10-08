using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MouseInputManager : MonoBehaviour
{
    internal const float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag
    internal Vector3 initialMousePosition;

    internal int isDragging(PlayerStateManager player)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
            float initialSpeed = player.projectileController.CalculateShootingPower(dragDistance);

            // Get mouse position in world coordinates
            Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

            if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                //Debug.Log("drag distance - " + dragDistance);
                if (dragDistance > dragThreshold)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            player.trajectoryLine.EndLine();
        }

        if (Input.GetMouseButton(0))
        {
            //Vector3 currentPoint = mousePosition;
            player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
        }

        return -1;
    }
}
