using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : PlayerComponents
{
    [SerializeField] internal float moveSpeed;
    [SerializeField] private float maxCLickDuration = 0.25f; // Maximum time the mouse button can be held down for a click to be registered

    private Vector3 targetPosition;


    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        targetPosition = transform.position;
    }

    internal void MovePlayer()
    {
        if (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
