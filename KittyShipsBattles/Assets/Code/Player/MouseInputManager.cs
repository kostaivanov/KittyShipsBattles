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

    internal bool isMouseDown = false;
    internal bool isMouseDragging = false;
    internal bool shouldShoot = false;



    internal bool isDragging(PlayerStateManager player)
    {

        player.mouseInputManager.mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
            isMouseDragging = false;
            shouldShoot = false;
            //if (allowedToShoot)
            //{
            player.mouseInputManager.dragDistance = 0f;
            //}
            player.mouseInputManager.initialMousePosition = player.mouseInputManager.mousePosition;
            //Debug.Log("initial mouse pos = " + player.mouseInputManager.initialMousePosition);
        }

        // Detect mouse button release and shoot projectile
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            //if (allowedToShoot)
            //{
            player.mouseInputManager.clickedWorldPosition = player.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            player.mouseInputManager.clickedWorldPosition.z = 0;
            //Debug.Log("clickedWorldPosition = " + clickedWorldPosition);

            player.mouseInputManager.dragDistance = Vector2.Distance(player.mouseInputManager.initialMousePosition, player.mouseInputManager.clickedWorldPosition);
            //Debug.Log("dragDistance = " + player.mouseInputManager.dragDistance);

            // Get mouse position in world coordinates
            Vector3 viewportPosition = player.mainCamera.WorldToViewportPoint(clickedWorldPosition);

            if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                if (isMouseDragging)
                {
                    shouldShoot = true;
                }

                isMouseDragging = false;
            }
            //}

        }

        if (isMouseDown == true && Input.GetMouseButton(0) && player.currentState != player.movingState)
        {
            if (player.mouseInputManager.dragDistance > dragThreshold)
            {
                isMouseDragging = true;
                player.trajectoryLine.RenderLine(initialMousePosition, mousePosition);
                return true;
            }
            //Vector3 currentPoint = mousePosition;

            //Debug.Log("initial mouse pos = " + initialMousePosition);
        }
        return false;
        //return -1;
    }

    //private void Update()
    //{
    //    Debug.Log(this.gameObject.name + " is dragging  = " + isDragging(GetComponent<PlayerStateManager>()));

    //}
}
