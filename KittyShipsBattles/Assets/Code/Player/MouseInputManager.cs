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

    internal bool isMouseDragging = false;
    internal bool hasClicked = false;
    PlayerStateManager player;
    private void Start()
    {
        player = GetComponent<PlayerStateManager>();
    }

    internal bool isDragging()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hasClicked = false;
        if (Input.GetMouseButtonDown(0))
        {
            dragDistance = 0;
            initialMousePosition = mousePosition;

            isMouseDragging = true;
            hasClicked = true;
        }

        if (Input.GetMouseButton(0) && player.currentState != player.movingState)
        {
            //isMouseDragging = true;
            //dragDistance = Vector2.Distance(initialMousePosition, mousePosition);

            // Render the trajectory line for dragging
            player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
            Debug.Log(dragDistance);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //hasClicked = false;
            clickedWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickedWorldPosition.z = 0;
            dragDistance = Vector2.Distance(initialMousePosition, clickedWorldPosition);
            Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

            if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                //Debug.Log("drag distance - " + dragDistance);
                if (dragDistance > dragThreshold)
                {
                    // 1 equals dragging for shooting
                    return true;
                }
                else
                {
                    // 0 equals just clicking for moving
                    return false;
                }
            }
        }
        dragDistance = 0;
        return false;
    }
}
