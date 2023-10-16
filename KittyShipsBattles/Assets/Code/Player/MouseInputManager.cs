using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MouseInputManager : MonoBehaviour
{
    internal const float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag
    [SerializeField] LayerMask overlapLayer;
    internal bool allowedToShoot = true;
    internal Vector3 initialMousePosition;
    internal float dragDistance;
    internal Vector3 clickedWorldPosition;
    internal Vector2 mousePosition;

    internal int isDragging(PlayerStateManager player)
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (allowedToShoot)
            {
                dragDistance = -1;
                initialMousePosition = mousePosition;
            }
            Debug.Log("initial mouse pos = " + initialMousePosition);
        }

        // Detect mouse button release and shoot projectile
        else if (Input.GetMouseButtonUp(0))
        {
            if (allowedToShoot)
            {
                clickedWorldPosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
                clickedWorldPosition.z = 0;

                dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);

                // Get mouse position in world coordinates
                Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

                if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
                {
                    //Debug.Log("drag distance - " + dragDistance);
                    if (dragDistance > dragThreshold)
                    {
                        // 1 equals dragging for shooting
                        return 1;
                    }
                    else
                    {
                        // 0 equals just clicking for moving
                        return 0;
                    }
                }
            }
            
        }

        if (Input.GetMouseButton(0) && player.currentState != player.movingState)
        {
            //Vector3 currentPoint = mousePosition;
            player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
            Debug.Log("initial mouse pos = " + initialMousePosition);

        }
        
        return -1;
    }
}
