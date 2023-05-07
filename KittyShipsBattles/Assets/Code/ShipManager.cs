using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ShipManager : MonoBehaviour
{
    [SerializeField] private float yAxisTolerance = 1.5f;
    private string shipTag = "Player";

    private List<GameObject> ships;

    // Start is called before the first frame update
    void Start()
    {
        UpdateShipList();
    }

    private void UpdateShipList()
    {
        ships = new List<GameObject>(GameObject.FindGameObjectsWithTag(shipTag));
    }

    internal bool IsValidTarget(GameObject shooter, GameObject target)
    {
        if (shooter == target)
        {
            return false; //Ignore self
        }

        float yDifference = Mathf.Abs(shooter.transform.position.y - target.transform.position.y);
        return yDifference <= yAxisTolerance;
    }
}
