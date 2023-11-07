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

    internal void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = mousePosition;
            isMouseDragging = false;
            hasClicked = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (Vector2.Distance(initialMousePosition, mousePosition) > dragThreshold)
            {
                isMouseDragging = true;
                hasClicked = false;
                // Render the trajectory line for dragging
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            clickedWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickedWorldPosition.z = 0;
        }
    }

    internal bool isDragging()
    {
        return isMouseDragging;
    }
}
