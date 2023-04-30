using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class TrajectoryLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    internal void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;
        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer.SetPositions(points);
    }

    internal void EndLine()
    {
        lineRenderer.positionCount = 0;
    }
}
