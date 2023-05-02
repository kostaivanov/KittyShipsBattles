using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : PlayerComponents
{
    [SerializeField] internal float moveSpeed;
    [SerializeField] private float maxCLickDuration = 0.25f; // Maximum time the mouse button can be held down for a click to be registered

    private Vector3 targetPosition;
    private Camera mainCamera;
    private float mouseDownTime;

    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        MovePlayer();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownTime = Time.time;
            Vector3 clickedPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            clickedPosition.z = 0; // Set z position to 0 to keep the player on the 2D plane

            // Check if the clicked position is within the camera boundaries
            Vector3 viewportPosition = mainCamera.WorldToViewportPoint(clickedPosition);
            if (viewportPosition.x >= 0 && viewportPosition.x <= 1 && viewportPosition.y >= 0 && viewportPosition.y <= 1)
            {
                targetPosition = clickedPosition;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            float clickDuration = Time.time - mouseDownTime;
            Vector3 mouseUpPosition = Input.mousePosition;
        }
    }

    private void MovePlayer()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
