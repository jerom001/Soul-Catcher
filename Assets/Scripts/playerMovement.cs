using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2D;
    public float minX = -8;
    public float maxX = 8;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float playerMovementX = Input.GetAxis("Horizontal");

        if (playerMovementX > 0 )
        {
            transform.right = Vector3.right;
        }
        else if (playerMovementX < 0 )
        {
            transform.right = Vector3.left;
        }

        rb2D.linearVelocity = new Vector2(playerMovementX * moveSpeed, rb2D.linearVelocity.y);

        float clampedX = Mathf.Clamp(rb2D.position.x, minX, maxX);
        rb2D.position = new Vector2(clampedX, rb2D.position.y);
    }
}
