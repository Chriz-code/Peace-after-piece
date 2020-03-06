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
        if(rb2D)
        rb2D.velocity = new Vector2(moveInput * speed, rb2D.velocity.y);
        if (moveInput < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * -1;

            this.gameObject.GetComponent<Transform>().localScale = scale;
        }
        else if (moveInput > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            this.gameObject.GetComponent<Transform>().localScale = scale;
        }
    }
}
