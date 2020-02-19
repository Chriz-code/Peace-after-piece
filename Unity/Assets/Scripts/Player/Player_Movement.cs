using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    public float moveInput;
    public float speed = 10f;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();

    }
    public void Movement(float horizontal)
    {
        moveInput = horizontal;
        rb2D.velocity = new Vector2(moveInput * speed, rb2D.velocity.y);
        if (moveInput < 0)
        {
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(-1f, 5f, 1f);
        }
        else if (moveInput > 0)
        {
            this.gameObject.GetComponent<Transform>().localScale = new Vector3(1f, 5f, 1f);
        }
    }
}
