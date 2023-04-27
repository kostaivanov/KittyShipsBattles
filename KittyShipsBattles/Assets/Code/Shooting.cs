using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
internal class Shooting : PlayerComponents
{
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject bullet;
    private GameObject clone;

    private PlayerHealth playerHealth;

    [SerializeField] internal float minPower;
    [SerializeField] internal float maxPower;

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 shootDirection;
    private float shootPower;
    private bool isDragging = false;


    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        if (Input.GetMouseButton(0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float slideDistance = Mathf.Clamp(startPoint.x - endPoint.x, 0f, float.MaxValue);
            float slideRatio = slideDistance / Screen.width;
            shootPower = Mathf.Lerp(minPower, maxPower, slideRatio);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootDirection = (startPoint - endPoint).normalized;
            shootPower = Mathf.Clamp((endPoint - startPoint).magnitude, minPower, maxPower);
            Shoot();
            isDragging = false;
        }
    }

    private void OnGUI()
    {
        if (isDragging == true)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            shootDirection = (endPoint - startPoint).normalized;
            shootPower = Mathf.Clamp((endPoint - startPoint).magnitude, minPower, maxPower);

            //Draw the power bar
            GUI.Box(new Rect(10, 10, 100, 20), "Power: " + shootPower.ToString("F1"));
        }

    }

    private void Shoot()
    {
        clone = Instantiate(bullet, transform.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = shootDirection * shootPower;

    } 
}
