using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClickHandler : MonoBehaviour
{
    [SerializeField] LayerMask overlapLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePosition, overlapLayer);

            if (hit != null)
            {
                PlayerStateManager player = hit.GetComponent<PlayerStateManager>();
                if (player != null)
                {
                    player.SwitchState(player.movingState);
                    Debug.Log("I am clicked!");
                }
            }
        }
    }
}
