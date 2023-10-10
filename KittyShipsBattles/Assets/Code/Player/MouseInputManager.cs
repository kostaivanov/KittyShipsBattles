using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MouseInputManager : MonoBehaviour
{
    internal const float dragThreshold = 0.5f; // Minimum distance the mouse needs to be dragged to be considered a drag
    [SerializeField] LayerMask overlapLayer;

    internal Vector3 initialMousePosition;
    internal float dragDistance;
    internal Vector3 clickedWorldPosition;
    internal Vector2 mousePosition;

    private void Update()
    {
    }

    internal int isDragging(PlayerStateManager player)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            dragDistance = -1;
            initialMousePosition = mousePosition;
        }

        // Detect mouse button release and shoot projectile
        else if (Input.GetMouseButtonUp(0))
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
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            //Vector3 currentPoint = mousePosition;
            player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
        }

        return -1;

    }
}
