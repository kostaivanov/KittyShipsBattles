using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
internal class Shooting : PlayerComponents
{
    [SerializeField] private Transform gunTip;
    [SerializeField] private GameObject bullet;
    private PlayerHealth playerHealth;


    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
