using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerMovement : PlayerComponents
{
    [SerializeField] internal float moveSpeed;
    [SerializeField] private float maxCLickDuration = 0.25f; // Maximum time the mouse button can be held down for a click to be registered
    private Coroutine movingCoroutine;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
        //targetPosition = transform.position;
    }

    internal void MovePlayer(Vector3 targetPosition)
    {
        if (movingCoroutine != null)
        {
            StopCoroutine(movingCoroutine);
        }
        movingCoroutine = StartCoroutine(MoveToTarget(targetPosition));
        //if (transform.position != targetPosition)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        //}
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Snap to the target position to avoid small inaccuracies
        transform.position = targetPosition;

        // Clear the moving coroutine reference
        movingCoroutine = null;
    }
}
