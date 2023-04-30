using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class TrajectoryLine : MonoBehaviour
{
    public float maxLength = 10f;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    internal void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = (endPoint - startPoint).normalized;
        float distance = Vector3.Distance(startPoint, endPoint);
        float clampedDistance = Mathf.Clamp(distance, 0, maxLength);

        Vector3 clampedEndPoint = startPoint + direction * clampedDistance;

        lineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = clampedEndPoint;

        lineRenderer.SetPositions(points);
    }

    internal void EndLine()
    {
        lineRenderer.positionCount = 0;
    }
}
