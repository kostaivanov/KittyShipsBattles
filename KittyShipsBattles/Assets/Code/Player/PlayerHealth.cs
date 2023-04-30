using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerHealth : PlayerComponents
{
    // Start is called before the first frame update
    internal override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Debug.Log("Hit the target!");
        if (otherObject.CompareTag("Projectile"))
        {
            otherObject.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
